using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Models
{
    /// <summary>
    /// 人员离职登记-确认离职
    /// </summary>
    public class OAPersonLeaveConfirmForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 人员id
        /// </summary>
        [Required]
        public Guid PersonId { get; set; }
    }
}
