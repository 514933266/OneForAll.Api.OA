using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneForAll.Core;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 团队成员异动历史查询服务
    /// </summary>
    public interface IOATeamMemberHistoryQueryService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="teamId"></param>
        /// <param name="key"></param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>结果</returns>
        Task<PageList<OATeamMemberHistory>> GetPageAsync(int pageIndex, int pageSize, Guid teamId, string key, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// 按登录用户id获取人事历程列表
        /// </summary>
        /// <param name="userId">登录用户id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberHistory>> GetListByUserIdAsync(Guid userId);
    }
}
