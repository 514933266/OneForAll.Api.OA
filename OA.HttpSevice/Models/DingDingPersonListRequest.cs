using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.HttpService.Models
{
    /// <summary>
    /// 钉钉人员档案
    /// </summary>
    public class DingDingPersonListRequest
    {
        /// <summary>
        /// 在职员工状态筛选，可以查询多个状态。不同状态之间使用英文逗号分隔。
        /// 2：试用期 3：正式 5：待离职 -1：无状态
        /// </summary>
        [JsonProperty(PropertyName = "status_list")]
        public string StatusList { get; set; }

        /// <summary>
        /// 分页游标，从0开始。根据返回结果里的next_cursor是否为空来判断是否还有下一页，且再次调用时offset设置成next_cursor的值。
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        /// <summary>
        /// 分页大小，最大50
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public int Size { get; set; }
    }
}
