using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.HttpService.Models
{
    /// <summary>
    /// 钉钉人员列表
    /// </summary>
    public class DingDingPersonListResponse
    {
        /// <summary>
        /// 返回码
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public DingDingPersonListPageResultResponse Result { get; set; }

        /// <summary>
        /// 返回码
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public int ErrCode { get; set; }

        /// <summary>
        /// 返回码描述
        /// </summary>
        [JsonProperty(PropertyName = "errmsg")]
        public string ErrMsg { get; set; }

        /// <summary>
        /// 是否调用成功
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// 请求ID
        /// </summary>
        public string request_id { get; set; }
    }

    public class DingDingPersonListPageResultResponse
    {
        /// <summary>
        /// 查询到的员工userid
        /// </summary>>
        [JsonProperty(PropertyName = "data_list")]
        public List<string> DataList { get; set; }

        /// <summary>
        /// 下一次分页调用的offset值，当返回结果里没有next_cursor时，表示分页结束。
        /// </summary>
        [JsonProperty(PropertyName = "next_cursor")]
        public int NextCursor { get; set; }
    }
}
