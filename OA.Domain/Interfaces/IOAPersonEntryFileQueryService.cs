using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OA.Domain.Aggregates;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 新员工入职档案查询服务
    /// </summary>
    public interface IOAPersonEntryFileQueryService
    {
        /// <summary>
        /// 获取档案设置列表
        /// </summary>
        /// <param name="id">入职档案id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPersonSettingAggr>> GetListSettingAsync(Guid id);

        /// <summary>
        /// 获取入职登记实体
        /// </summary>
        /// <param name="id">入职档案id</param>
        /// <returns>实体</returns>
        Task<OAPersonEntry> GetAsync(Guid id);
    }
}
