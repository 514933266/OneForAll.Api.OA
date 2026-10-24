using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OA.Domain.Aggregates;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 员工合同查询服务
    /// </summary>
    public interface IOAPersonContractQueryService
    {
        /// <summary>
        /// 获取近二个月合同即将过期/已过期列表
        /// </summary>
        /// <param name="teamId">所属部门id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPersonContractAggr>> GetListAsync(Guid teamId);
    }
}
