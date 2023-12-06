using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 员工生日关怀
    /// </summary>
    public class OAPersonBirthdayCareDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所在部门
        /// </summary>
        public IEnumerable<OAPersonTeamDto> Teams { get; set; }

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
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
    }
}
