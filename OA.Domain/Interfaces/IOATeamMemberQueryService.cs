using System;
using System.Threading.Tasks;
using OA.Domain.Aggregates;
using OA.Domain.AggregateRoots;
using System.Collections.Generic;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 部门成员查询服务
    /// </summary>
    public interface IOATeamMemberQueryService
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
        /// 查询指定人员的团队关联记录（含团队信息）
        /// </summary>
        /// <param name="personId">人员id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberAggr>> GetListByPersonAsync(Guid personId);

        /// <summary>
        /// 查询当前用户所在的团队关联记录
        /// </summary>
        /// <param name="loginUserId">当前登录用户id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAMidTeamPerson>> GetListByUserAsync(Guid loginUserId);

        /// <summary>
        /// 查询团队成员数量
        /// </summary>
        /// <param name="teamIds">团队id</param>
        /// <returns>数量</returns>
        Task<int> GetCountByTeamAsync(IEnumerable<Guid> teamIds);
    }
}
