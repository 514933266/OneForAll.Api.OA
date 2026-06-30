using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Enums
{
    /// <summary>
    /// 通知类型
    /// </summary>
    public enum OANotificationTypeEnum
    {
        /// <summary>
        /// 系统通知
        /// </summary>
        System = 0,

        /// <summary>
        /// 邮件通知
        /// </summary>
        Email = 1,

        /// <summary>
        /// 短信通知
        /// </summary>
        Sms = 2,

        /// <summary>
        /// 微信公众号通知
        /// </summary>
        Wxgzh = 3
    }
}
