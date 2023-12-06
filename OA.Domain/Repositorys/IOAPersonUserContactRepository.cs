using OA.Domain.AggregateRoots;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Repositorys
{
    /// <summary>
    /// 人员档案
    /// </summary>
    public interface IOAPersonUserContactRepository : IEFCoreRepository<OAPersonUserContact>
    {
        /// <summary>
        /// 查询人员档案
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>人员</returns>
        Task<OAPerson> GetByUserIdAsync(Guid userId);
    }
}
