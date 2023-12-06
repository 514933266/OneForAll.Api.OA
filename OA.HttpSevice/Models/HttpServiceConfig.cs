using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.HttpService.Models
{
    /// <summary>
    /// 数据资源服务配置
    /// </summary>
    public class HttpServiceConfig
    {
        /// <summary>
        /// 权限验证接口
        /// </summary>
        public string SysPermissionCheck { get; set; } = "SysPermissionCheck";

        /// <summary>
        /// Api日志
        /// </summary>
        public string SysApiLog { get; set; } = "SysApiLog";

        /// <summary>
        /// 异常日志
        /// </summary>
        public string SysExceptionLog { get; set; } = "SysExceptionLog";

        /// <summary>
        /// 操作日志
        /// </summary>
        public string SysOperationLog { get; set; } = "SysOperationLog";

        /// <summary>
        /// 定时任务调度中心
        /// </summary>
        public string ScheduleJob { get; set; } = "ScheduleJob";

        /// <summary>
        /// 消息通知
        /// </summary>
        public string UmsMessage { get; set; } = "UmsMessage";

        /// <summary>
        /// 微信关注用户
        /// </summary>
        public string SysWxgzhNotifyUser { get; set; } = "SysWxgzhNotifyUser";

        /// <summary>
        /// 微信公众号模板消息
        /// </summary>
        public string WxgzhTemplateMessage { get; set; } = "WxgzhTemplateMessage";
    }
}
