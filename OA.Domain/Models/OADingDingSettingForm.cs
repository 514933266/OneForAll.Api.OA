using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Models
{
    /// <summary>
    /// 钉钉对接设置
    /// </summary>
    public class OADingDingSettingForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 企业id
        /// </summary>
        [Required]
        [StringLength(200)]
        public string CompanyId { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        [Required]
        [StringLength(200)]
        public string AgentId { get; set; }

        /// <summary>
        /// 应用key
        /// </summary>
        [Required]
        [StringLength(200)]
        public string AppKey { get; set; }

        /// <summary>
        /// 应用秘钥
        /// </summary>
        [Required]
        [StringLength(200)]
        public string AppSecret { get; set; }

        /// <summary>
        /// 同步人员档案
        /// </summary>
        [Required]
        public bool SyncPerson { get; set; }

        /// <summary>
        /// 同步组织架构
        /// </summary>
        [Required]
        public bool SyncTeam { get; set; }
    }
}
