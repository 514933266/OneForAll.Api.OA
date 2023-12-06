using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Models
{
    /// <summary>
    /// 办理转正
    /// </summary>
    public class OAPersonFormalConfirmForm
    {
        /// <summary>
        /// 人员档案id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 实际转正日期
        /// </summary>
        [Required]
        public DateTime ActualEntryDate { get; set; }
    }
}
