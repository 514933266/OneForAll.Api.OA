using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.EFCore;
using OA.Domain.AggregateRoots;
using OneForAll.Core;

namespace OA.Domain.Repositorys
{
    /// <summary>
    /// 仓储：部门成员历史
    /// </summary>
    public interface IOATeamMemberHistoryRepository : IEFCoreRepository<OATeamMemberHistory>
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="teamId"></param>
        /// <param name="key"></param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>列表</returns>
        Task<PageList<OATeamMemberHistory>> GetPageAsync(
            int pageIndex,
            int pageSize,
            Guid teamId,
            string key,
            DateTime? startDate,
            DateTime? endDate);

        /// <summary>
        /// 查询个人历程
        /// </summary>
        /// <param name="userId">系统用户id</param>
        /// <returns></returns>
        Task<IEnumerable<OATeamMemberHistory>> GetListByUserIdAsync(Guid userId);
    }
}
