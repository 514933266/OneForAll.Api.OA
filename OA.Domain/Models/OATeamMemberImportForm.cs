using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Models
{
    /// <summary>
    /// 部门成员导入Excel
    /// </summary>
    public class OATeamMemberImportForm
    {
        /// <summary>
        /// 工号
        /// </summary>
        [StringLength(20, ErrorMessage = "工号最大长度为20个字符")]
        public string WorkNumber { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 人员职级
        /// </summary>
        [StringLength(20, ErrorMessage = "员工职级最大长度为20个字符")]
        public string Job { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(20, ErrorMessage = "身份证号最大长度为20个字符")]
        public string IdCard { get; set; }

        /// <summary>
        /// 所属团队
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 是否主管
        /// </summary>
        public string IsLeader { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(20, ErrorMessage = "电话最大长度为20个字符")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(20, ErrorMessage = "邮箱最大长度为20个字符")]
        public string Email { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public string EntryDate { get; set; }

        /// <summary>
        /// 员工类型
        /// </summary>
        public string EmployeeType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500, ErrorMessage = "备注最大长度为500个字符")]
        public string Remark { get; set; }

    }
}
