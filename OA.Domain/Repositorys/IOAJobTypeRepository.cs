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
    }
}
