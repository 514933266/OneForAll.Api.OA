using AutoMapper;
using Newtonsoft.Json.Linq;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Enums;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OA.HttpService.Enums;
using OA.HttpService.Interfaces;
using OA.HttpService.Models;
using OneForAll.Core;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 个人日程
    /// </summary>
    public class OAPersonalScheduleService : IOAPersonalScheduleService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonalScheduleManager _manager;
        private readonly IUmsMessageHttpService _umsHttpService;
        private readonly ISysWxUserHttpService _wxUserHttpService;
        public OAPersonalScheduleService(
            IMapper mapper,
            IOAPersonalScheduleManager manager,
            IUmsMessageHttpService umsHttpService,
            ISysWxUserHttpService wxUserHttpService)
        {
            _mapper = mapper;
            _manager = manager;
            _umsHttpService = umsHttpService;
            _wxUserHttpService = wxUserHttpService;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="date">月份</param>
        /// <param name="isClosed">是否已关闭</param>
        /// <returns>分页列表</returns>
        public async Task<IEnumerable<OAPersonalScheduleDto>> GetListAsync(DateTime date, bool? isClosed)
        {
            var data = await _manager.GetListAsync(date, isClosed);
            return _mapper.Map<IEnumerable<OAPersonalSchedule>, IEnumerable<OAPersonalScheduleDto>>(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAPersonalScheduleForm form)
        {
            return await _manager.AddAsync(form);
        }

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">项目id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _manager.DeleteAsync(ids);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="id">日程id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> CloseAsync(Guid id)
        {
            return await _manager.CloseAsync(id);
        }

        /// <summary>
        /// 巡检待提醒日程并发送通知，返回已提醒的日程数
        /// </summary>
        /// <param name="wxgzhTemplateId">公众号模板id（为空则不发送公众号提醒）</param>
        /// <returns>已提醒的日程数</returns>
        public async Task<int> MonitorNotifyAsync(string wxgzhTemplateId)
        {
            var num = 0;
            var now = DateTime.Now;
            var closedIds = new List<Guid>();
            var schedules = await _manager.GetListNeedNotifyAsync(now);
            foreach (var schedule in schedules)
            {
                if (now < schedule.NotifyTime)
                    continue;

                var closed = false;
                var types = schedule.NotificationTypeJson.FromJson<List<OANotificationTypeEnum>>();
                if (types != null)
                {
                    foreach (var type in types)
                    {
                        var msg = await SendNotificationAsync(type, schedule, wxgzhTemplateId);
                        if (msg != null && msg.Status)
                            closed = true;
                    }
                }

                if (closed)
                {
                    num++;
                    closedIds.Add(schedule.Id);
                }
            }

            if (closedIds.Any())
                await _manager.CloseRangeAsync(closedIds);

            return num;
        }

        /// <summary>
        /// 按通知渠道发送提醒
        /// </summary>
        /// <param name="type">通知类型</param>
        /// <param name="schedule">日程</param>
        /// <param name="wxgzhTemplateId">公众号模板id</param>
        /// <returns>结果</returns>
        private async Task<BaseMessage> SendNotificationAsync(OANotificationTypeEnum type, OAPersonalSchedule schedule, string wxgzhTemplateId)
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
                    msg = await SendWxgzhNotificationAsync(schedule, wxgzhTemplateId);
                    break;
            }
            return msg;
        }

        /// <summary>
        /// 系统通知
        /// </summary>
        /// <param name="schedule">日程</param>
        /// <returns>结果</returns>
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

        /// <summary>
        /// 微信公众号通知
        /// </summary>
        /// <param name="schedule">日程</param>
        /// <param name="wxgzhTemplateId">公众号模板id</param>
        /// <returns>结果</returns>
        private async Task<BaseMessage> SendWxgzhNotificationAsync(OAPersonalSchedule schedule, string wxgzhTemplateId)
        {
            if (!wxgzhTemplateId.IsNullOrEmpty())
            {
                var wxUser = await _wxUserHttpService.GetSysWxgzhUsersAsync(schedule.SysUserId);
                if (wxUser != null && !wxUser.OpenId.IsNullOrEmpty())
                {
                    var date = schedule.SettingDate.ToString("yyyy-MM-dd");
                    return await _umsHttpService.SendWxgzhAsync(new WechatGzhTemplateRequest()
                    {
                        TemplateId = wxgzhTemplateId,
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
            return new BaseMessage();
        }
    }
}
