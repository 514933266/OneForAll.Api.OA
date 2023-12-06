using OneForAll.Core;
using OA.HttpService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.HttpService.Interfaces
{
    /// <summary>
    /// 消息通知
    /// </summary>
    public interface IUmsMessageHttpService
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns></returns>
        Task<BaseMessage> SendAsync(UmsMessageRequest form);

        /// <summary>
        /// 发送微信公众号模板消息消息
        /// </summary>
        /// <param name="request">推送请求</param>
        /// <returns></returns>
        Task<BaseMessage> SendWxgzhAsync(WechatGzhTemplateRequest request);
    }
}
