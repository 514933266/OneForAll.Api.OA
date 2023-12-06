using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 关联表：团队组织->人员资料
    /// </summary>
    public class OATeamPersonContact : AggregateRoot<Guid>, ICreateTime
    {
        /// <summary>
        /// 所属机构Id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// 团队组织Id
        /// </summary>
        [Required]
        public Guid OATeamId { get; set; }

        /// <summary>
        /// 人员Id
        /// </summary>
        [Required]
        public Guid OAPersonId { get; set; }

        /// <summary>
        /// 是否管理者
        /// </summary>
        [Required]
        public bool IsLeader { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 数据更新时间(用于离职或调动)
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
