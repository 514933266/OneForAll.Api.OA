using OA.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Aggregates
{
    /// <summary>
    /// 离职登记
    /// </summary>
    public class OAPersonLeaveAggr : OAPersonLeave
    {
        /// <summary>
        /// 加入部门
        /// </summary>
        public OATeam OATeam { get; set; }

        /// <summary>
        /// 人员信息
        /// </summary>
        public OAPerson OAPerson { get; set; }
    }
}
