using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 钉钉接入设置
    /// </summary>
    public class OADingDingSetting : AggregateRoot<Guid>, ICreateTime, ICreator<Guid>, IUpdateTime
    {
        /// <summary>
        /// 租户id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

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

        /// <summary>
        /// 创建人id
        /// </summary>
        [Required]
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        [Required]
        [StringLength(20)]
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; } = DateTime.Now;
    }
}
