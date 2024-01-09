using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OA.HttpService.Interfaces;
using OA.HttpService.Models;
using OA.Public.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace OA.HttpService
{
    /// <summary>
    /// 消息通知
    /// </summary>
    public class UmsMessageHttpService : BaseHttpService, IUmsMessageHttpService
    {
        private readonly HttpServiceConfig _config;

        public UmsMessageHttpService(
            HttpServiceConfig config,
            IHttpContextAccessor httpContext,
            IHttpClientFactory httpClientFactory) : base(httpContext, httpClientFactory)
        {
            _config = config;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns></returns>
        public async Task<BaseMessage> SendAsync(UmsMessageRequest form)
        {
            var client = GetHttpClient(_config.UmsMessage);
            if (client != null)
            {
                var msg = await client.PostAsync(client.BaseAddress, form, new JsonMediaTypeFormatter());
                var content = await msg.Content.ReadAsStringAsync();
                return content.FromJson<BaseMessage>();
            }

            throw new Exception("客户端配置异常");
        }

        /// <summary>
        /// 发送微信公众号模板消息消息
        /// </summary>
        /// <param name="request">推送请求</param>
        /// <returns></returns>
        public async Task<BaseMessage> SendWxgzhAsync(WechatGzhTemplateRequest request)
        {
            var client = GetHttpClient(_config.WxgzhTemplateMessage);
            if (client == null)
                throw new Exception("客户端配置异常");

            var response = await client.PostAsync(client.BaseAddress, request, new JsonMediaTypeFormatter());
            return await response.Content.ReadFromJsonAsync<BaseMessage>();
        }
    }
}
