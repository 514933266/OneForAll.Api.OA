using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OA.Domain.Models
{
    /// <summary>
    /// 员工入职
    /// </summary>
    public class OAPersonEntryForm
    {

        public Guid Id { get; set; }

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
        /// 预计入职时间
        /// </summary>
        [Required]
        public DateTime EstimateEntryDate { get; set; }
    }
}
