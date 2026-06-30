using Microsoft.AspNetCore.Http;
using OA.HttpService.Interfaces;
using OA.HttpService.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace OA.HttpService
{
    /// <summary>
    /// 全局异常日志
    /// </summary>
    public class SysGlobalExceptionLogHttpService : BaseHttpService, ISysGlobalExceptionLogHttpService
    {
        private readonly HttpServiceConfig _config;

        public SysGlobalExceptionLogHttpService(
            HttpServiceConfig config,
            IHttpContextAccessor httpContext,
            IHttpClientFactory httpClientFactory) : base(httpContext, httpClientFactory)
        {
            _config = config;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns></returns>
        public async Task AddAsync(SysGlobalExceptionLogRequest form)
        {
            form.CreateTime = DateTime.UtcNow;

            var client = GetHttpClient(_config.SysLog);
            if (client != null && client.BaseAddress != null)
            {
                await client.PostAsync("api/SysGlobalExceptionLogs", form, new JsonMediaTypeFormatter());
            }
        }
    }
}