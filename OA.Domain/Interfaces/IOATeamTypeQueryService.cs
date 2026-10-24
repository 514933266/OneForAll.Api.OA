using System.Threading.Tasks;
using OA.Domain.AggregateRoots;
using System.Collections.Generic;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 部门类型查询服务
    /// </summary>
    public interface IOATeamTypeQueryService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamType>> GetListAsync(string name);
    }
}
