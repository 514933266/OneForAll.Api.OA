using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OA.Domain.Aggregates;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 员工转正查询服务
    /// </summary>
    public interface IOAPersonFormalQueryService
    {
        /// <summary>
        /// 获取待转正列表
        /// </summary>
        /// <param name="name">员工姓名</param>
        /// <param name="teamId">团队id</param>
        /// <param name="planStartDate">计划转正-开始日期</param>
        /// <param name="planEndDate">计划转正-开始日期</param>
        /// <param name="actualStartDate">实际转正-开始日期</param>
        /// <param name="actualEndDate">实际转正-开始日期</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPersonFormalAggr>> GetListAsync(
            string name,
            Guid teamId,
            DateTime? planStartDate,
            DateTime? planEndDate,
            DateTime? actualStartDate,
            DateTime? actualEndDate);
    }
}
