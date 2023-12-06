using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 人员数量统计
    /// </summary>
    public class OAPersonStatisticV2Dto
    {
        /// <summary>
        /// 在职员工数量
        /// </summary>
        public int TotalOnJobCount { get; set; }

        /// <summary>
        /// 正式员工数量
        /// </summary>
        public int NormalCount { get; set; }

        /// <summary>
        /// 试用员工数量
        /// </summary>
        public int TrialCount { get; set; }

        /// <summary>
        /// 实习生数量
        /// </summary>
        public int InternCount { get; set; }

        /// <summary>
        /// 当前部门员工数量
        /// </summary>
        public int TeamCount { get; set; }

        /// <summary>
        /// 当前组员工数量
        /// </summary>
        public int GroupCount { get; set; }
    }
}
