using OA.Domain.Enums;
using OA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 离职登记
    /// </summary>
    public class OAPersonLeaveDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 人员id
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>

        public string PersonName { get; set; }

        /// <summary>
        /// 人员职级
        /// </summary>

        public string PersonJob { get; set; }

        /// <summary>
        /// 员工类型
        /// </summary>
        public string EmployeeType { get; set; }

        /// <summary>
        /// 部门
        /// </summary>

        public string TeamName { get; set; }

        /// <summary>
        /// 预计离职时间
        /// </summary>
        public DateTime EstimateLeaveDate { get; set; }

        /// <summary>
        /// 离职原因
        /// </summary>
        public List<string> Reasons { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
