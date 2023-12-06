using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.HttpService.Models
{
    /// <summary>
    /// 微信关注用户
    /// </summary>
    public class SysWxgzhSubscribeUserResponse
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 微信用户OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 订阅时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 关注方式
        /// </summary>
        public int SubscribeType { get; set; }

        /// <summary>
        /// 是否已取消订阅
        /// </summary>
        public bool IsUnSubscribed { get; set; }

        /// <summary>
        /// AccessToken
        /// </summary>
        public string AppAccessToken { get; set; }
    }
}
