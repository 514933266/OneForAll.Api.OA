using OA.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Aggregates
{
    /// <summary>
    /// 人员入职登记
    /// </summary>
    public class OAPersonEntryAggr : OAPersonEntry
    {
        /// <summary>
        /// 加入部门
        /// </summary>
        public OATeam OATeam { get; set; }

        /// <summary>
        /// 是否超期
        /// </summary>
        public bool IsOverdue { get; set; }

        /// <summary>
        /// 超期天数
        /// </summary>
        public int OverdueDays { get; set; }
    }
}
