using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OA.Domain.Models
{
    /// <summary>
    /// 表单：团队
    /// </summary>
    public class OATeamForm : Entity<Guid>
    {

        /// <summary>
        /// 上级id
        /// </summary>
        [Required]
        public Guid ParentId { get; set; }

        /// <summary>
        /// 直属负责人id
        /// </summary>
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
        [StringLength(20)]
        public string Type { get; set; }
    }
}
