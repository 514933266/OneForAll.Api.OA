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
using Newtonsoft.Json.Linq;
using OneForAll.Core.Utility;
using OA.Domain.AggregateRoots;
using System.Runtime.InteropServices.JavaScript;

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
            var msg = new BaseMessage();
            var schedules = await _scheduleRepository.GetListIQFAsync(DateTime.Now);
            foreach (var schedule in schedules)
            {
                if (DateTime.Now < schedule.NotifyTime)
                    continue;
                var closed = false;
                var types = schedule.NotificationTypeJson.FromJson<List<OANotificationTypeEnum>>();
                types.ForEach(type =>
                {
                    msg = SendNotificationAsync(type, schedule).Result;
                    if (msg.Status)
                        closed = true;
                });

                if (closed)
                {
                    num++;
                    schedule.IsClosed = true;
                }
            }
            await _scheduleRepository.SaveChangesAsync();
            await _jobHttpService.LogAsync(_config.ClientCode, typeof(MonitorPersonlScheduleJob).Name, $"巡检个人日程任务执行完成，共发送{num}条提醒");
        }

        private async Task<BaseMessage> SendNotificationAsync(OANotificationTypeEnum type, OAPersonalSchedule schedule)
        {
            var msg = new BaseMessage();
            switch (type)
            {
                case OANotificationTypeEnum.System:
                    msg = await SendSystemNotificationAsync(schedule);
                    break;
                case OANotificationTypeEnum.Email:
                    break;
                case OANotificationTypeEnum.Sms:
                    break;
                case OANotificationTypeEnum.Wxgzh:
                    msg = await SendWxgzhNotificationAsync(schedule);
                    break;
            }
            return msg;
        }

        // 系统通知
        private async Task<BaseMessage> SendSystemNotificationAsync(OAPersonalSchedule schedule)
        {
            return await _umsHttpService.SendAsync(new UmsMessageRequest()
            {
                Title = "个人事项",
                Content = schedule.Content,
                ToAccountId = schedule.SysUserId,
                Type = UmsMessageTypeEnum.Default
            });
        }

        private async Task<BaseMessage> SendWxgzhNotificationAsync(OAPersonalSchedule schedule)
        {
            var templateId = _configuration["Wxgzh:ScheduleMsgTemplateId"];
            if (!templateId.IsNullOrEmpty())
            {
                var wxUser = await _wxUserHttpService.GetSysWxgzhUsersAsync(schedule.SysUserId);
                if (wxUser != null && !wxUser.OpenId.IsNullOrEmpty())
                {
                    var date = schedule.SettingDate.ToString("yyyy-MM-dd");
                    return await _umsHttpService.SendWxgzhAsync(new WechatGzhTemplateRequest()
                    {
                        TemplateId = templateId,
                        AccessToken = wxUser.AppAccessToken,
                        ToUser = wxUser.OpenId,
                        Url = "pages/tab/home",
                        Data = new JObject
                        {
                            ["thing9"] = new JObject() { ["value"] = "重要事项提醒", ["color"] = "#0000ff" },
                            ["time10"] = new JObject() { ["value"] = date, ["color"] = "#0000ff" },
                            ["thing4"] = new JObject() { ["value"] = schedule.Content, ["color"] = "#0000ff" }
                        }
                    });
                }
            }
            return default(BaseMessage);
        }
    }
}
