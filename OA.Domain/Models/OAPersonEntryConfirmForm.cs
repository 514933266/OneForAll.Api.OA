using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Models
{
    /// <summary>
    /// 入职登记-确认到岗
    /// </summary>
    public class OAPersonEntryConfirmForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [StringLength(20)]
        public string WorkNumber { get; set; }

        /// <summary>
        /// 员工类型
        /// </summary>
        [Required]
        public string EmployeeType { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        [Required]
        public string EmployeeStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }
    }
}
