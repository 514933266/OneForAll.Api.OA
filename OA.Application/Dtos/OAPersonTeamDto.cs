using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 人员档案-团队
    /// </summary>
    public class OAPersonTeamDto
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 是否主管
        /// </summary>
        public string IsLeader { get; set; }
    }
}
