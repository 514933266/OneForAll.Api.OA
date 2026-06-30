using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 钉钉对接设置
    /// </summary>
    public class OADingDingSettingDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 企业id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        public string AgentId { get; set; }

        /// <summary>
        /// 应用key
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 应用秘钥
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 同步人员档案
        /// </summary>
        public bool SyncPerson { get; set; }

        /// <summary>
        /// 同步组织架构
        /// </summary>
        public bool SyncTeam { get; set; }
    }
}
