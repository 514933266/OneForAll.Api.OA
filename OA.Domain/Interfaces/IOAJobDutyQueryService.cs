using OneForAll.Core;
using System.Threading.Tasks;
using OA.Domain.AggregateRoots;
using System.Collections.Generic;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 职务查询服务
    /// </summary>
    public interface IOAJobDutyQueryService
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<PageList<OAJobDuty>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAJobDuty>> GetListAsync(string key);
    }
}
