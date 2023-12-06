using OA.Domain.AggregateRoots;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Aggregates
{
    /// <summary>
    /// 企业组织架构树
    /// </summary>
    public class OATeamTreeAggr : OATeam, IParent<Guid>, IEntity<Guid>, IChildren<OATeamTreeAggr>
    {
        public OATeamTreeAggr()
        {
            Children = new HashSet<OATeamTreeAggr>();
        }

        /// <summary>
        /// 直属主管姓名
        /// </summary>
        public string LeaderName { get; set; }

        /// <summary>
        /// 团队人数
        /// </summary>
        public int MemberNumber { get; set; }

        /// <summary>
        /// 子级
        /// </summary>

        public virtual IEnumerable<OATeamTreeAggr> Children { get; set; }

    }
}

