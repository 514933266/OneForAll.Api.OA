using System;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OneForAll.EFCore;
using OA.Domain.Enums;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;

namespace OA.Domain.Repositorys
{
    /// <summary>
    /// 职位管理
    /// </summary>
    public interface IOAJobRepository : IEFCoreRepository<OAJob>
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>m>
        /// <returns>分页列表</returns>
        Task<PageList<OAJob>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="isEnabled">是否启用</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAJobAggr>> GetListAsync(bool? isEnabled, string key);
    }
}
