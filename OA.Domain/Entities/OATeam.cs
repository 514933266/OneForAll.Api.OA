using OneForAll.Core;
using OneForAll.Core.ORM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 基础表：组织架构
    /// </summary>
    public class OATeam : IEntity<Guid>, IParent<Guid>
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 所属机构id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        [Required]
        public Guid ParentId { get; set; }

        /// <summary>
        /// 直属主管Id
        /// </summary>
        [Required]
        public Guid LeaderId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Type { get; set; } = "";

        /// <summary>
        /// 排序号 由大到小
        /// </summary>
        [Required]
        public int SortNumber { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }

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
        public DateTime CreateTime { get; set; }
    }
}
