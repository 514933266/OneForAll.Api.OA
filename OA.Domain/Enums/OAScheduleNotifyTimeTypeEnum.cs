using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Enums
{
    /// <summary>
    /// 日程时间类型
    /// </summary>
    public enum OAScheduleNotifyTimeTypeEnum
    {
        /// <summary>
        /// 提前3天
        /// </summary>
        ThreeDays = 0,

        /// <summary>
        /// 提前1天
        /// </summary>
        OneDays = 1,

        /// <summary>
        /// 自定义
        /// </summary>
        Other = 99
    }
}
