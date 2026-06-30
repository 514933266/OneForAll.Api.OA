using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.EFCore;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Repositorys
{
    /// <summary>
    /// 仓储：部门类型
    /// </summary>
    public interface IOATeamTypeRepository : IEFCoreRepository<OATeamType>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamType>> GetListAsync(string name);

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>列表</returns>
        Task<OATeamType> GetAsync(string name);
    }
}
