using OA.Domain.Enums;
using OA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 人员
    /// </summary>
    public class OAPersonDto
    {
        public OAPersonDto()
        {
            Teams = new List<OAPersonTeamDto>();
            ExtendInformations = new List<OAPersonExtenInformationFieldVo>();
        }

        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所在部门
        /// </summary>
        public List<OAPersonTeamDto> Teams { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string WorkNumber { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// 员工类型
        /// </summary>
        public string EmployeeType { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        public string EmployeeStatus { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// 性别 0女 1男
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? EntryDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? LeaveDate { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 司龄
        /// </summary>
        public decimal JoinAge { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public List<OAPersonExtenInformationFieldVo> ExtendInformations { get; set; }
    }
}
