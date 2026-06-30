using OA.Application.Dtos;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 团队成员异动历史
    /// </summary>
    public interface IOATeamMemberHistoryService
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
        Task<PageList<OATeamMemberHistoryDto>> GetPageAsync(
            int pageIndex,
            int pageSize,
            Guid teamId,
            string key,
            DateTime? startDate,
            DateTime? endDate);
    }
}
