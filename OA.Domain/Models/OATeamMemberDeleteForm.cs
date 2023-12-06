using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Models
{
    /// <summary>
    /// 表单：移除团队成员
    /// </summary>
    public class OATeamMemberDeleteForm
    {
        /// <summary>
        /// 团队id
        /// </summary>
        [Required]
        public Guid TeamId { get; set; }

        /// <summary>
        /// 是否离职
        /// </summary>
        public bool IsLeave { get; set; }

        /// <summary>
        /// ContactId
        /// </summary>
        [Required]
        public IEnumerable<Guid> Ids { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
