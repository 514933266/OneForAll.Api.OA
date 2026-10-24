using System;
using System.Threading.Tasks;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Enums;
using System.Collections.Generic;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 企业组织架构查询服务
    /// </summary>
    public interface IOATeamQueryService
    {
        /// <summary>
        /// 获取列表树
        /// </summary>
        /// <param name="parentId">上级id</param>
        /// <param name="type">类型</param>
        /// <param name="deep">是否深度检索</param>
        /// <param name="scope">范围 -1全部 0有效 1被删除数据</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamTreeAggr>> GetListAsync(Guid parentId, string type, bool deep, OATeamSearchScopeEnum scope);

        /// <summary>
        /// 获取指定组织
        /// </summary>
        /// <param name="id">组织id</param>
        /// <returns>列表</returns>
        Task<OATeam> GetAsync(Guid id);

        /// <summary>
        /// 获取有效组织列表
        /// </summary>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeam>> GetListValidAsync();

        /// <summary>
        /// 按组织id集合获取组织列表
        /// </summary>
        /// <param name="ids">组织id集合</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeam>> GetListAsync(IEnumerable<Guid> ids);
    }
}
