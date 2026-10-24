using System.Threading.Tasks;
using OA.Domain.AggregateRoots;
using System.Collections.Generic;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 职级类型查询服务
    /// </summary>
    public interface IOAJobTypeQueryService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAJobType>> GetListAsync(string key);
    }
}
