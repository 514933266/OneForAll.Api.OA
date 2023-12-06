using OA.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Aggregates
{
    /// <summary>
    /// 人员合同
    /// </summary>
    public class OAPersonContractAggr : OAPerson
    {
        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime ContractFirstDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime ContractEndDate { get; set; }

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractType { get; set; }

        /// <summary>
        /// 合同公司
        /// </summary>
        public string ContractCompany { get; set; }

        /// <summary>
        /// 直属团队
        /// </summary>
        public OATeam OATeam { get; set; }
    }
}
