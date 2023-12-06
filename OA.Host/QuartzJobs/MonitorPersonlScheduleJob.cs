using Quartz;
using OA.Host.Models;
using System.Threading.Tasks;
using OA.Domain.Repositorys;
using OA.HttpService.Interfaces;
using System;
using System.Linq;
using OA.Repository;
using Quartz.Impl;
using NuGet.Protocol;
using OA.Domain.Enums;
using System.Collections.Generic;
using OA.HttpService.Models;
using OA.HttpService.Enums;
using OneForAll.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace OA.Host.QuartzJobs
{
    /// <summary>
    /// 监控个人日程
    /// </summary>
    public class MonitorPersonlScheduleJob : IJob
    {
        private readonly AuthConfig _config;
        private readonly IConfiguration _configuration;
        private readonly IScheduleJobHttpService _jobHttpService;
        private readonly IUmsMessageHttpService _umsHttpService;
        private readonly ISysWxUserHttpService _wxUserHttpService;
        private readonly IOAPersonalScheduleRepository _scheduleRepository;
        public MonitorPersonlScheduleJob(
            AuthConfig config,
            IConfiguration configuration,
            IScheduleJobHttpService jobHttpService,
            IUmsMessageHttpService umsHttpService,
            ISysWxUserHttpService wxUserHttpService,
            IOAPersonalScheduleRepository scheduleRepository)
        {
            _config = config;
            _configuration = configuration;
            _jobHttpService = jobHttpService;
            _umsHttpService = umsHttpService;
            _wxUserHttpService = wxUserHttpService;
            _scheduleRepository = scheduleRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var num = 0;
            var firstDate = DateTime.Now.Date;
            var lastDate = DateTime.Now.AddDays(1).Date;
            var schedules = await _scheduleRepository.GetListAsync(w => !w.IsClosed && w.NotifyTime >= firstDate && w.NotifyTime < lastDate);
            foreach (var schedule in schedules)
            {
                if (schedule.SettingDate.Hour <= DateTime.Now.Hour)
                {
                    var types = schedule.NotificationTypeJson.FromJson<List<OANotificationTypeEnum>>();
                    if (types.Any(w => w == OANotificationTypeEnum.System))
                    {
                        types.ForEach(async type =>
                        {
                            await SendNotificationAsync(type, schedule.SysUserId, schedule.Content);
                        });
                    }
                    else
                    {
                        // 默认必带系统提醒
                        await SendNotificationAsync(OANotificationTypeEnum.System, schedule.SysUserId, schedule.Content);
                        types.ForEach(async type =>
                        {
                            await SendNotificationAsync(type, schedule.SysUserId, schedule.Content);
                        });
                    }
                }
            }
            await _jobHttpService.LogAsync(_config.ClientCode, typeof(MonitorPersonlScheduleJob).Name, $"巡检个人日程任务执行完成，共发送{num}条提醒");
        }

        private async Task<BaseMessage> SendNotificationAsync(OANotificationTypeEnum type, Guid userId, string content)
        {
            var msg = new BaseMessage();
            switch (type)
            {
                case OANotificationTypeEnum.System:
                    msg = await _umsHttpService.SendAsync(new UmsMessageRequest()
                    {
                        Title = "日程提醒",
                        Content = content,
                        ToAccountId = userId,
                        Type = UmsMessageTypeEnum.Default
                    });
                    break;
                case OANotificationTypeEnum.Email:
                    break;
                case OANotificationTypeEnum.Wxgzh:
                    var templateId = _configuration["Wxgzh:ScheduleMsgTemplateId"];
                    if (!templateId.IsNullOrEmpty())
                    {
                        var wxUser = await _wxUserHttpService.GetSysWxgzhUsersAsync(userId);
                        if (wxUser != null)
                        {
                            msg = await _umsHttpService.SendWxgzhAsync(new WechatGzhTemplateRequest()
                            {
                                TemplateId = templateId,
                                AccessToken = wxUser.AppAccessToken,
                                ToUser = wxUser.OpenId,
                                Data = new Dictionary<string, string>()
                                {
                                    { "Content", content }
                                }
                            });
                        }
                    }
                    break;
                case OANotificationTypeEnum.Sms:
                    break;
            }
            return msg;
        }
    }
}
