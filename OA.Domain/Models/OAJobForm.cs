using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OA.Domain.Models
{
    /// <summary>
    /// 职位管理
    /// </summary>
    public class OAJobForm
    {
        public Guid Id { get; set; }

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
        public Guid TypeId { get; set; }

        /// <summary>
        /// 职级id
        /// </summary>
        [Required]
        public Guid LevelId { get; set; }

        /// <summary>
        /// 职务id
        /// </summary>
        [Required]
        public Guid DutyId { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [Required]
        public Guid TeamId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; }

    }
}
