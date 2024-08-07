using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OA.Domain.Models
{
    /// <summary>
    /// 离职登记
    /// </summary>
    public class OAPersonLeaveForm
    {
        
        public Guid Id { get; set; }

        /// <summary>
        /// 人员id
        /// </summary>
        [Required]
        public Guid PersonId { get; set; }

        /// <summary>
        /// 预计离职时间
        /// </summary>
        [Required]
        public DateTime EstimateLeaveDate { get; set; }

        /// <summary>
        /// 离职原因
        /// </summary>
        [Required]
        public List<string> Reasons { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }
    }
}
