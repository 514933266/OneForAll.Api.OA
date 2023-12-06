using OA.Domain.Enums;
using OA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OA.Domain.Models
{
    /// <summary>
    /// 表单：人员
    /// </summary>
    public class OAPersonForm
    {
        public OAPersonForm()
        {
            ExtendInformations = new List<OAPersonExtenInformationFieldVo>();
        }
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(20)]
        public string IdCard { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(50)]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        [StringLength(20)]
        public string Job { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }

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
        /// 离职日期
        /// </summary>
        public DateTime? LeaveDate { get; set; }

        /// <summary>
        /// 员工类型
        /// </summary>
        public string EmployeeType { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        public string EmployeeStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public List<OAPersonExtenInformationFieldVo> ExtendInformations { get; set; }

    }
}
