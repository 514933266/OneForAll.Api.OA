using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OA.Domain.Aggregates;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 员工入职查询服务
    /// </summary>
    public interface IOAPersonEntryQueryService
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<PageList<OAPersonEntryAggr>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="creatorName">创建人</param>
        /// <param name="mobilePhone">手机号码</param>
        /// <param name="startDate">开始入职日期</param>
        /// <param name="endDate">开始入职日期</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPersonEntryAggr>> GetListAsync(string name, string creatorName, string mobilePhone, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// 查询指定id数据
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        Task<OAPersonEntryAggr> GetAsync(Guid id);
    }
}
