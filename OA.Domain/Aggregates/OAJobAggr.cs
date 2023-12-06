using OA.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Aggregates
{
    /// <summary>
    /// 岗位职级
    /// </summary>
    public class OAJobAggr : OAJob
    {
        /// <summary>
        /// 类别
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        public string LevelName { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string DutyName { get; set; }

        /// <summary>
        /// 团队
        /// </summary>
        public string TeamName { get; set; }
    }
}
