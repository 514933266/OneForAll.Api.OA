using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 职务管理
    /// </summary>
    public class OAJobDuty
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
        /// 名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Remark { get; set; } = "";
    }
}
