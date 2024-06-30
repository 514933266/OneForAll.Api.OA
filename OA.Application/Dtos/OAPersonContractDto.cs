using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 合同管理
    /// </summary>
    public class OAPersonContractDto
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

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
        /// 部门
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime ContractFirstDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime ContractEndDate { get; set; }

        /// <summary>
        /// 合同公司
        /// </summary>
        public string ContractCompany { get; set; }

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractType { get; set; }
    }
}
