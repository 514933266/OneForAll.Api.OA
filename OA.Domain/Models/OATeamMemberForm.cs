using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Models
{
    /// <summary>
    /// 表单：团队人员
    /// </summary>
    public class OATeamMemberForm
    {
        /// <summary>
        /// ContactId
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 团队id
        /// </summary>
        [Required]
        public Guid TeamId { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 人员职级
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Job { get; set; }

        /// <summary>
        /// 人员性别
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(20)]
        public string IdCard { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(20)]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(20)]
        public string Email { get; set; }

        /// <summary>
        /// 是否主管
        /// </summary>
        public bool IsLeader { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [StringLength(20)]
        public string WorkNumber { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? EntryDate { get; set; }

        /// <summary>
        /// 员工类型
        /// </summary>
        public string EmployeeType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }
    }
}
