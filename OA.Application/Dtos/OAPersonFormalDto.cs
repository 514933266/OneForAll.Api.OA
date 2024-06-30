using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 转正管理
    /// </summary>
    public class OAPersonFormalDto
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
        /// 入职日期
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// 计划转正日期
        /// </summary>
        public DateTime? PlanEntryDate { get; set; }

        /// <summary>
        /// 实际转正日期
        /// </summary>
        public DateTime? ActualEntryDate { get; set; }

        /// <summary>
        /// 试用期
        /// </summary>
        public string TryDate { get; set; }
    }
}
