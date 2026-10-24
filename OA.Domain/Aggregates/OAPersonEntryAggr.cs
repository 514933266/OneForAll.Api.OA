using OA.Domain.AggregateRoots;
using System;

namespace OA.Domain.Aggregates
{
    /// <summary>
    /// 人员入职登记（含到岗逾期判定）
    /// </summary>
    public class OAPersonEntryAggr : OAPersonEntry
    {
        /// <summary>
        /// 距预计到岗日的逾期天数（未逾期为 0）
        /// </summary>
        public int OverdueDays
        {
            get
            {
                var days = (int)(DateTime.Now - EstimateEntryDate).TotalDays;
                return days >= 1 ? days : 0;
            }
        }

        /// <summary>
        /// 是否已过预计到岗日
        /// </summary>
        public bool IsOverdue
        {
            get { return OverdueDays >= 1; }
        }
    }
}
