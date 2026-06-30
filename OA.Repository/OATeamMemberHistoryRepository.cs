using Microsoft.EntityFrameworkCore;
using OA.Domain.AggregateRoots;
using OA.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    /// <summary>
    /// 部门成员历史
    /// </summary>
    public class OATeamMemberHistoryRepository : Repository<OATeamMemberHistory>, IOATeamMemberHistoryRepository
    {
        public OATeamMemberHistoryRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="teamId"></param>
        /// <param name="key"></param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>列表</returns>
        public async Task<PageList<OATeamMemberHistory>> GetPageAsync(
            int pageIndex,
            int pageSize,
            Guid teamId,
            string key,
            DateTime? startDate,
            DateTime? endDate)
        {
            var predicate = PredicateBuilder.Create<OATeamMemberHistory>(w => true);

            if (teamId != Guid.Empty) predicate = predicate.And(w => w.OATeamId == teamId || w.TargetOATeamId == teamId);
            if (!key.IsNullOrEmpty()) predicate = predicate.And(w => w.PersonName.StartsWith(key));
            if (startDate != null) predicate = predicate.And(w => w.CreateTime >= startDate);
            if (endDate != null) predicate = predicate.And(w => w.CreateTime <= endDate);

            var total = await DbSet.CountAsync(predicate);

            var data = await DbSet
                .AsNoTracking()
                .Where(predicate)
                .OrderByDescending(o => o.CreateTime)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageList<OATeamMemberHistory>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询个人历程
        /// </summary>
        /// <param name="userId">系统用户id</param>
        /// <returns></returns>
        public async Task<IEnumerable<OATeamMemberHistory>> GetListByUserIdAsync(Guid userId)
        {
            var personDbSet = Context.Set<OAPerson>().Where(w => w.SysUserId == userId);
            var sql = (from history in DbSet
                       join person in personDbSet on history.OAPersonId equals person.Id
                       select history);

            return await sql.OrderByDescending(o => o.CreateTime).ToListAsync();
        }
    }
}
