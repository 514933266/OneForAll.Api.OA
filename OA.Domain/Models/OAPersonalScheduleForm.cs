using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Domain.Enums;

namespace OA.Domain.Models
{
    /// <summary>
    /// 个人日程
    /// </summary>
    public class OAPersonalScheduleForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 提醒内容
        /// </summary>
        [Required]
        [StringLength(300)]
        public string Content { get; set; }

        /// <summary>
        /// 通知类型,OANotificationTypeEnum的字符串储存形式 
        /// </summary>
        [Required]
        public List<OANotificationTypeEnum> NotificationTypes { get; set; }

        /// <summary>
        /// 日程时间
        /// </summary>
        [Required]
        public DateTime SettingDate { get; set; }

        /// <summary>
        /// 提醒时间类型
        /// </summary>
        [Required]
        public OAScheduleNotifyTimeTypeEnum NotifyTimeType { get; set; }

        /// <summary>
        /// 提前多少小时
        /// </summary>
        public int NotifyTimeSpan { get; set; }
    }
}
