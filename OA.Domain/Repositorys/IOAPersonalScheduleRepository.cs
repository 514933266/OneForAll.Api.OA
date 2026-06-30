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
    /// 个人日程
    /// </summary>
    public interface IOAPersonalScheduleRepository : IEFCoreRepository<OAPersonalSchedule>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OAPersonalSchedule>> GetListAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="date">通知时间</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OAPersonalSchedule>> GetListIQFAsync(DateTime date);
    }
}
