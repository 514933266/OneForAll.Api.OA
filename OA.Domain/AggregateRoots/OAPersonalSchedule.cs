using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OA.Domain.Enums;
using OA.Public.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 个人日程
    /// </summary>
    public class OAPersonalSchedule
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 所属机构Id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// 通知用户id
        /// </summary>
        [Required]
        public Guid SysUserId { get; set; }

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
        public string NotificationTypeJson { get; set; }

        /// <summary>
        /// 提醒时间类型
        /// </summary>
        [Required]
        public OAScheduleNotifyTimeTypeEnum NotifyTimeType { get; set; } = OAScheduleNotifyTimeTypeEnum.Other;

        /// <summary>
        /// 提前多少小时
        /// </summary>
        [Required]
        public int NotifyTimeSpan { get; set; }

        /// <summary>
        /// 日程时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime SettingDate { get; set; }

        /// <summary>
        /// 提醒时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime NotifyTime { get; set; }

        /// <summary>
        /// 已关闭
        /// </summary>
        [Required]
        public bool IsClosed { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        [Required]
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        [Required]
        [StringLength(20)]
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 个人日程通知
        /// </summary>
        /// <param name="loginUser">登录用户</param>
        public void InitPersonalNotification(LoginUser loginUser)
        {
            if (loginUser != null)
            {
                SysUserId = loginUser.Id;
                CreatorId = loginUser.Id;
                CreatorName = loginUser.Name;
                SysTenantId = loginUser.SysTenantId;
            }
        }

        /// <summary>
        /// 设置通知时间
        /// </summary>
        /// <param name="timeType">时间类型</param>
        /// <param name="otherTime">提前x小时</param>
        public void CalculateNotifyTime()
        {
            switch (NotifyTimeType)
            {
                case OAScheduleNotifyTimeTypeEnum.ThreeDays:
                    NotifyTime = SettingDate.AddDays(-3);
                    break;
                case OAScheduleNotifyTimeTypeEnum.OneDays:
                    NotifyTime = SettingDate.AddDays(-1);
                    break;
                case OAScheduleNotifyTimeTypeEnum.Other:
                    NotifyTime = NotifyTimeSpan > 0 ? SettingDate.AddHours(NotifyTimeSpan) : SettingDate;
                    break;
            }
        }
    }
}
