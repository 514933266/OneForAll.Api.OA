using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OA.Domain.Enums;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 个人日程
    /// </summary>
    public class OAPersonalScheduleDto
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 提醒内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 通知类型,OANotificationTypeEnum的字符串储存形式 
        /// </summary>
        public List<OANotificationTypeEnum> NotificationTypes { get; set; }

        /// <summary>
        /// 提醒时间类型
        /// </summary>
        public OAScheduleNotifyTimeTypeEnum NotifyTimeType { get; set; }

        /// <summary>
        /// 提前多少小时
        /// </summary>
        public int NotifyTimeSpan { get; set; }

        /// <summary>
        /// 日程时间
        /// </summary>]
        public DateTime SettingDate { get; set; }

        /// <summary>
        /// 提醒时间
        /// </summary>
        public DateTime NotifyTime { get; set; }

        /// <summary>
        /// 已关闭
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>

        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
