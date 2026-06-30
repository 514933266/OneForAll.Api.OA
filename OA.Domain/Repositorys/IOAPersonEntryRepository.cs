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
    /// 员工入职
    /// </summary>
    public interface IOAPersonEntryRepository : IEFCoreRepository<OAPersonEntry>
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>m>
        /// <returns>分页列表</returns>
        Task<PageList<OAPersonEntry>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="creatorName">创建人</param>
        /// <param name="mobilePhone">手机号码</param>
        /// <param name="startDate">开始入职日期</param>
        /// <param name="endDate">开始入职日期</param>
        /// <returns></returns>
        Task<IEnumerable<OAPersonEntry>> GetListAsync(
             string name,
             string creatorName,
             string mobilePhone,
             DateTime? startDate,
             DateTime? endDate);

        /// <summary>
        /// 查询指定id数据
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        Task<OAPersonEntry> GetIQFAsync(Guid id);
    }
}
