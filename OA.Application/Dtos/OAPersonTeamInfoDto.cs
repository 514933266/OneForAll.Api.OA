using OA.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 人员团队信息
    /// </summary>
    public class OAPersonTeamInfoDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        public string Job { get; set; }

        public virtual List<OATeamDto> Teams { get; set; } = new List<OATeamDto>();
    }
}
