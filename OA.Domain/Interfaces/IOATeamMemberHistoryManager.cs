using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OA.Domain.Models;
using OA.Domain.Aggregates;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 团队成员异动历史
    /// </summary>
    public interface IOATeamMemberHistoryManager
    {
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

        /// <summary>
        /// 办理离职时批量生成异动日志
        /// </summary>
        /// <param name="members">成员列表（含团队）</param>
        /// <param name="leave">离职登记</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddRangeOnLeaveAsync(IEnumerable<OATeamMemberAggr> members, OAPersonLeave leave);
    }
}
