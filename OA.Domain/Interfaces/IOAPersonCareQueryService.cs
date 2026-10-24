using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OA.Domain.Aggregates;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 员工关怀查询服务
    /// </summary>
    public interface IOAPersonCareQueryService
    {
        /// <summary>
        /// 获取员工生日列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberAggr>> GetListBirthdayAsync(Guid teamId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// 获取入职周年列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="date">日期</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberAggr>> GetListCompanyAsync(Guid teamId, DateTime date);
    }
}
