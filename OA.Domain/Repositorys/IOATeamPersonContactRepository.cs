using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.ValueObjects;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Repositorys
{
    /// <summary>
    /// 仓储：团队组织人员
    /// </summary>
    public interface IOATeamPersonContactRepository : IEFCoreRepository<OAMidTeamPerson>
    {
        /// <summary>
        /// 查询列表（含人员基础信息）
        /// </summary>
        /// <param name="teamId">组织节点id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberAggr>> GetListByTeamAsync(Guid teamId);

        /// <summary>
        /// 查询列表（含人员基础信息）
        /// </summary>
        /// <param name="teamIds">团队id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberAggr>> GetListByTeamAsync(IEnumerable<Guid> teamIds);

        /// <summary>
        /// 查询列表（含人员基础信息）
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="personIds">人员id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberAggr>> GetListByTeamAsync(Guid teamId, IEnumerable<Guid> personIds);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="name">员工姓名</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberAggr>> GetListByTeamAsync(Guid teamId, string name);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="personId">人员档案id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberAggr>> GetListByPersonAsync(Guid personId);

        /// <summary>
        /// 查询团队成员数量
        /// </summary>
        /// <param name="teamIds">团队id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberCountVo>> GetListCountByTeamAsync(IEnumerable<Guid> teamIds);

        /// <summary>
        /// 查询团队成员数量
        /// </summary>
        /// <param name="teamIds">团队id</param>
        /// <returns>列表</returns>
        Task<int> GetCountByTeamAsync(IEnumerable<Guid> teamIds);

        /// <summary>
        /// 查询当前用户所在的团队
        /// </summary>
        /// <param name="loginUserId">当前登录用户id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAMidTeamPerson>> GetListByUserAsync(Guid loginUserId);

        /// <summary>
        /// 按团队id查询关联记录（跟踪）
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <returns>列表</returns>
        Task<List<OAMidTeamPerson>> GetListByTeamIdAsync(Guid teamId);

        /// <summary>
        /// 按团队id和人员id查询关联记录（跟踪）
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="personId">人员id</param>
        /// <returns>关联记录</returns>
        Task<OAMidTeamPerson> GetByTeamAndPersonAsync(Guid teamId, Guid personId);

        /// <summary>
        /// 按关联表主键id集合查询（跟踪）
        /// </summary>
        /// <param name="ids">关联表id集合</param>
        /// <returns>列表</returns>
        Task<List<OAMidTeamPerson>> GetListByIdsAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 按人员id集合查询关联记录（跟踪）
        /// </summary>
        /// <param name="personIds">人员id集合</param>
        /// <returns>列表</returns>
        Task<List<OAMidTeamPerson>> GetListByPersonIdsAsync(IEnumerable<Guid> personIds);

        /// <summary>
        /// 按单个人员id查询关联记录
        /// </summary>
        /// <param name="personId">人员id</param>
        /// <returns>列表</returns>
        Task<List<OAMidTeamPerson>> GetListByPersonIdAsync(Guid personId);
    }
}
