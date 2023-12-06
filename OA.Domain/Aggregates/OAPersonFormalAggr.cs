using OA.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Aggregates
{
    /// <summary>
    /// 转正管理
    /// </summary>
    public class OAPersonFormalAggr : OAPerson
    {
        /// <summary>
        /// 计划转正日期
        /// </summary>
        public DateTime PlanEntryDate { get; set; }

        /// <summary>
        /// 实际转正日期
        /// </summary>
        public DateTime ActualEntryDate { get; set; }

        /// <summary>
        /// 试用期
        /// </summary>
        public string TryDate { get; set; }

        /// <summary>
        /// 直属团队
        /// </summary>
        public OATeam OATeam { get; set; }
    }
}
