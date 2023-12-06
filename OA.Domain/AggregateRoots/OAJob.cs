using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 职位管理
    /// </summary>
    public class OAJob
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
        /// 类别id
        /// </summary>
        [Required]
        public Guid OAJobTypeId { get; set; } = Guid.Empty;

        /// <summary>
        /// 职级id
        /// </summary>
        [Required]
        public Guid OAJobLevelId { get; set; } = Guid.Empty;

        /// <summary>
        /// 职务id
        /// </summary>
        [Required]
        public Guid OAJobDutyId { get; set; } = Guid.Empty;

        /// <summary>
        /// 所属部门
        /// </summary>
        [Required]
        public Guid OATeamId { get; set; } = Guid.Empty;

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 启用菜单
        /// </summary>
        public void SetIsEnabled()
        {
            IsEnabled = !IsEnabled;
        }
    }
}
