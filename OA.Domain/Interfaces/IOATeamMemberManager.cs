using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OA.Domain.Models;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 团队成员
    /// </summary>
    public interface IOATeamMemberManager
    {
        /// <summary>
        /// 查询在职列表（含人员基础信息）
        /// </summary>
        /// <param name="teamId">组织id</param>
        /// <param name="deep">是否递归</param>
        /// <returns>成员列表</returns>
        Task<IEnumerable<OATeamMemberAggr>> GetListAsync(Guid teamId, bool deep);

        /// <summary>
        /// 查询未加入团队列表
        /// </summary>
        /// <returns>成员列表</returns>
        Task<IEnumerable<OAPerson>> GetListNoTeamAsync();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OATeamMemberForm entity);

        /// <summary>
        /// 添加（批量）
        /// </summary>
        /// <param name="teamId">组织id</param>
        /// <param name="personId">人员id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid teamId, IEnumerable<Guid> personId);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="member">成员表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OATeamMemberForm member);

        /// <summary>
        /// 移除（批量）
        /// </summary>
        /// <param name="teamId">人员id</param>
        /// <param name="personIds">关联表id集合</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid teamId, IEnumerable<Guid> personIds);

        /// <summary>
        /// 移除（批量）
        /// </summary>
        /// <param name="ids">关联表id集合</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 解散移除
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DissolveAsync(Guid teamId);

        /// <summary>
        /// 设置管理（批量）
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="personIds">关联id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> SetLeaderAsync(Guid teamId, IEnumerable<Guid> personIds);

        /// <summary>
        /// 人员档案删除（批量）
        /// </summary>
        /// <param name="personIds">关联id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> RemoveFileAsync(IEnumerable<Guid> personIds);

        /// <summary>
        /// 人员离职（批量）
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="personIds">关联id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> LeaveAsync(Guid teamId, IEnumerable<Guid> personIds);

        /// <summary>
        /// 人员调动（批量）
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="targetTeamId">目标团队id</param>
        /// <param name="contactIds">关联id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> TransferAsync(Guid teamId, Guid targetTeamId, IEnumerable<Guid> contactIds);

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="teamId">部门id</param>
        /// <param name="data">Excel人员列表</param>
        /// <returns>结果</returns>
        Task<BaseMessage> ImportExcelAsync(Guid teamId, IEnumerable<OATeamMemberImportForm> data);
    }
}
