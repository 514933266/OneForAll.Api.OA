using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OA.Domain.Models
{
    /// <summary>
    /// 表单：团队成员调动
    /// </summary>
    public class OATeamMemberTransferForm
    {
        /// <summary>
        /// 团队id
        /// </summary>
        [Required]
        public Guid TeamId { get; set; }

        /// <summary>
        /// 目标团队id
        /// </summary>
        [Required]
        public Guid TargetTeamId { get; set; }

        /// <summary>
        /// ContactId
        /// </summary>
        [Required]
        public IEnumerable<Guid> Ids { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }
    }
}
