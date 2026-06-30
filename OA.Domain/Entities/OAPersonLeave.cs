using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 人员离职登记
    /// </summary>
    public class OAPersonLeave
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// 所属机构Id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// 人员id
        /// </summary>
        [Required]
        public Guid OAPersonId { get; set; }

        /// <summary>
        /// 所在部门
        /// </summary>
        [Required]
        public string TeamName { get; set; } = "";

        /// <summary>
        /// 预计离职时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime EstimateLeaveDate { get; set; }

        /// <summary>
        /// 离职原因
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Reason { get; set; }

        /// <summary>
        /// 是否生成异动记录
        /// </summary>
        [Required]
        public bool CanCreateHistory { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Remark { get; set; } = "";

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
    }
}
