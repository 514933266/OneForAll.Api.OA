using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Enums;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Repositorys
{
    /// <summary>
    /// 仓储：团队组织
    /// </summary>
    public interface IOATeamRepository : IEFCoreRepository<OATeam>
    {
        #region 列表

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">上级id</param>
        /// <returns>团队列表</returns>
        Task<IEnumerable<OATeam>> GetListAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 查询列表（未删除）
        /// </summary>
        /// <returns>团队列表</returns>
        Task<IEnumerable<OATeam>> GetListValidAsync();

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="parentId">上级id</param>
        /// <param name="type">类型</param>
        /// <param name="scope">范围 -1全部 0有效 1被删除数据</param>
        /// <returns>团队列表</returns>
        Task<IEnumerable<OATeam>> GetListAsync(Guid parentId, string type, OATeamSearchScopeEnum scope);

        #endregion

        #region 实体

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        Task<OATeam> GetAsync(Guid id);

        /// <summary>
        /// 查询指定团队（含人员关联信息）
        /// </summary>
        /// <param name="id">组织架构id</param>
        /// <returns>实体</returns>
        Task<OATeam> GetWithContactAsync(Guid id);

        #endregion

        #region 其他

        /// <summary>
        /// 是否有子级
        /// </summary>
        /// <param name="id">上级id</param>
        /// <returns>结果</returns>
        Task<bool> HasChildrenAsync(Guid id);

        #endregion
    }
}
