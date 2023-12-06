using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OA.HttpService.Interfaces;
using OA.HttpService.Models;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OA.HttpService
{
    /// <summary>
    /// 消息通知
    /// </summary>
    public class SysWxUserHttpService : BaseHttpService, ISysWxUserHttpService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpServiceConfig _config;

        public SysWxUserHttpService(
            IConfiguration configuration,
            HttpServiceConfig config,
            IHttpContextAccessor httpContext,
            IHttpClientFactory httpClientFactory) : base(httpContext, httpClientFactory)
        {
            _config = config;
            _configuration = configuration;
        }

        /// <summary>
        /// 获取用户公众号信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public async Task<SysWxgzhSubscribeUserResponse> GetSysWxgzhUsersAsync(Guid userId)
        {
            var client = GetHttpClient(_config.SysWxgzhNotifyUser);
            if (client == null)
                throw new Exception("客户端配置异常");

            var wxClientId = _configuration["Wxgzh:ClientId"];
            var msg = await client.GetAsync(client.BaseAddress + $"?clientId={wxClientId}&userId={userId}");
            if (msg.StatusCode != System.Net.HttpStatusCode.OK)
                return new SysWxgzhSubscribeUserResponse();

            var result = msg.Content.ReadAsStringAsync()?.Result;
            var data = JsonConvert.DeserializeObject<SysWxgzhSubscribeUserResponse>(result);
            return data;
        }
    }
}
