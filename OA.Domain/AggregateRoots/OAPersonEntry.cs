using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    /// 人员入职
    /// </summary>
    public class OAPersonEntry
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// 所属机构Id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [Required]
        public Guid TeamId { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Job { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        [StringLength(20)]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 已交入职档案
        /// </summary>
        [Required]
        public bool IsSubmitEntryFile { get; set; }

        /// <summary>
        /// 人员档案信息，当IsSubmitEntryFile = true 时有值
        /// </summary>
        [Required]
        public string ExtendInformationJson { get; set; } = "";

        /// <summary>
        /// 预计入职时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime EstimateEntryDate { get; set; }

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
