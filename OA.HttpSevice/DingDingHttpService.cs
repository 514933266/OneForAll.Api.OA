using OA.HttpService.Interfaces;
using OA.HttpService.Models;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace OA.HttpService
{
    /// <summary>
    /// 钉钉同步
    /// </summary>
    public class DingDingHttpService : IDingDingHttpService
    {
        /// <summary>
        /// 获取在职员工列表
        /// </summary>
        public async Task<DingDingPersonListResponse> GetListPerson(string accessToken)
        {
            var client = new HttpClient();
            var url = "https://oapi.dingtalk.com/topapi/smartwork/hrm/employee/queryonjobt?" + accessToken;
            int offset = 0;
            int nextCur = 0;
            while (nextCur > 0)
            {
                var param = new DingDingPersonListRequest()
                {
                    StatusList = "3",
                    Offset = offset,
                    Size = 50,
                };
                var res = await client.PostAsync(url, new StringContent(param.ToJson()), new JsonMediaTypeFormatter());
                var result = await res.Content.ReadAsAsync<DingDingPersonListResponse>();
                if (result.ErrCode == 0)
                {
                    nextCur = result.Result.NextCursor;
                }
            };
            return default;
        }
    }
}
