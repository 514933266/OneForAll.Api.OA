using OA.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Aggregates
{
    /// <summary>
    /// 团队成员
    /// </summary>
    public class OATeamMemberAggr : OAPerson
    {
        /// <summary>
        /// 直属团队
        /// </summary>
        public OATeam OATeam { get; set; }

        /// <summary>
        /// 关联信息
        /// </summary>
        public OATeamPersonContact Contact { get; set; }
    }
}
