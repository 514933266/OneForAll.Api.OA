using Microsoft.AspNetCore.Http;
using OA.HttpService.Interfaces;
using OA.HttpService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core;

namespace OA.HttpService
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class SysOperationLogHttpService : BaseHttpService, ISysOperationLogHttpService
    {
        private readonly HttpServiceConfig _config;

        public SysOperationLogHttpService(
            HttpServiceConfig config,
            IHttpContextAccessor httpContext,
            IHttpClientFactory httpClientFactory) : base(httpContext, httpClientFactory)
        {
            _config = config;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task AddAsync(SysOperationLogRequest entity)
        {
            var client = GetHttpClient(_config.SysLog);
            if (client != null && client.BaseAddress != null)
            {
                await client.PostAsync("api/SysOperationLogs", entity, new JsonMediaTypeFormatter());
            }
        }
    }
}
