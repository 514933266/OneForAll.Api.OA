using System;
using System.Collections.Generic;
using System.Linq;
using OA.Domain.AggregateRoots;
using OA.Domain.ValueObjects;

namespace OA.Domain.Models
{
    /// <summary>
    /// 在职人员统计（领域统计口径）
    /// </summary>
    public class OAPersonStatistic
    {
        /// <summary>
        /// 在职总人数
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
        /// 我管理的部门人数
        /// </summary>
        public int TeamCount { get; set; }

        /// <summary>
        /// 我管理的群组人数
        /// </summary>
        public int GroupCount { get; set; }

        /// <summary>
        /// 按领域口径统计在职人员
        /// </summary>
        /// <param name="persons">在职人员列表</param>
        /// <returns>统计结果</returns>
        public static OAPersonStatistic Count(IEnumerable<OAPerson> persons)
        {
            var list = persons == null ? new List<OAPerson>() : persons.ToList();
            return new OAPersonStatistic()
            {
                TotalOnJobCount = list.Count,
                NormalCount = list.Count(w => w.EmployeeStatus == OAPersonEmployeeStatusVo.ValueNormal),
                TrialCount = list.Count(w => w.EmployeeStatus == OAPersonEmployeeStatusVo.ValueTrial),
                InternCount = list.Count(w => w.EmployeeType == OAPersonEmployeeTypeVo.ValueIntern),
            };
        }
    }
}
