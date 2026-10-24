using System;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OneForAll.EFCore;
using OA.Domain.Enums;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Repositorys
{
    /// <summary>
    /// 职级类型
    /// </summary>
    public interface IOAJobTypeRepository : IEFCoreRepository<OAJobType>
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>m>
        /// <returns>分页列表</returns>
        Task<PageList<OAJobType>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 按名称查询
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>实体</returns>
        Task<OAJobType> GetByNameAsync(string name);

        /// <summary>
        /// 按名称模糊查询
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        Task<List<OAJobType>> GetListByNameContainsAsync(string key);

        /// <summary>
        /// 按id集合查询
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns>列表</returns>
        Task<List<OAJobType>> GetListByIdsAsync(IEnumerable<Guid> ids);
    }
}
