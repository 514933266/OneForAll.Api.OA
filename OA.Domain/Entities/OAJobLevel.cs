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
    /// 职级管理
    /// </summary>
    public class OAJobLevel
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
        /// 名称
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(9,3)")]
        public decimal BaseSalary { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Remark { get; set; } = "";
    }
}
