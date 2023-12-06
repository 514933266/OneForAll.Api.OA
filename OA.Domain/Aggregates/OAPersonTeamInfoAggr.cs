using OA.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Aggregates
{
    /// <summary>
    /// 人员团队信息
    /// </summary>
    public class OAPersonTeamInfoAggr : OAPerson
    {
        public OAPersonTeamInfoAggr()
        {
            Teams = new List<OATeamTreeAggr>();
        }

        /// <summary>
        /// 所属团队集合 
        /// </summary>
        public virtual List<OATeamTreeAggr> Teams { get; set; }

    }
}
