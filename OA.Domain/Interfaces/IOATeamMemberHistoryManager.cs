using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OA.Domain.Models;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 团队成员异动历史
    /// </summary>
    public interface IOATeamMemberHistoryManager
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
        /// 添加入职历史
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OATeamMemberForm form);

        /// <summary>
        /// 添加调动历史
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OATeamMemberTransferForm form);

        /// <summary>
        /// 添加离职历史
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OATeamMemberDeleteForm form);
    }
}
