using Microsoft.EntityFrameworkCore;
using OA.Domain.AggregateRoots;
using OA.Domain.Repositorys;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    /// <summary>
    /// 个人日程
    /// </summary>
    public class OAPersonalScheduleRepository : Repository<OAPersonalSchedule>, IOAPersonalScheduleRepository
    {
        public OAPersonalScheduleRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OAPersonalSchedule>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await DbSet.Where(w => ids.Contains(w.Id)).ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="date">通知时间</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OAPersonalSchedule>> GetListIQFAsync(DateTime date)
        {
            var firstDate = date.Date;
            var lastDate = date.AddDays(1).Date;
            return await DbSet.IgnoreQueryFilters().Where(w => !w.IsClosed && w.NotifyTime >= firstDate && w.NotifyTime < lastDate).ToListAsync();
        }
    }
}
