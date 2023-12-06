using Microsoft.EntityFrameworkCore;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Enums;
using OA.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    /// <summary>
    /// 职位管理
    /// </summary>
    public class OAJobRepository : Repository<OAJob>, IOAJobRepository
    {
        public OAJobRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<OAJob>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<OAJob>(w => true);
            if (!key.IsNullOrEmpty())
            {
                predicate = predicate.And(w => w.Name.Contains(key));
            }

            var total = await DbSet.CountAsync(predicate);

            var data = await DbSet
                .Where(predicate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageList<OAJob>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="isEnabled">是否启用</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAJobAggr>> GetListAsync(bool? isEnabled, string key)
        {
            var predicate = PredicateBuilder.Create<OAJob>(w => true);
            if (!key.IsNullOrEmpty())
            {
                predicate = predicate.And(w => w.Name.Contains(key));
            }
            if (isEnabled.HasValue)
            {
                predicate = predicate.And(w => w.IsEnabled == isEnabled.Value);
            }

            var teamDbSet = Context.Set<OATeam>();
            var typeDbSet = Context.Set<OAJobType>();
            var levelDbSet = Context.Set<OAJobLevel>();
            var dutyDbSet = Context.Set<OAJobDuty>();

            var sql = (from job in DbSet.Where(predicate)
                       join team in teamDbSet on job.OATeamId equals team.Id into leftJoinTeam
                       from lfTeam in leftJoinTeam.DefaultIfEmpty()
                       join type in typeDbSet on job.OAJobTypeId equals type.Id into leftJoinType
                       from lfType in leftJoinType.DefaultIfEmpty()
                       join level in levelDbSet on job.OAJobLevelId equals level.Id into leftJoinLevel
                       from lfLevel in leftJoinLevel.DefaultIfEmpty()
                       join duty in dutyDbSet on job.OAJobDutyId equals duty.Id into leftJoinDuty
                       from lfDuty in leftJoinDuty.DefaultIfEmpty()
                       select new OAJobAggr()
                       {
                           Id = job.Id,
                           Name = job.Name,
                           OATeamId = job.OATeamId,
                           SysTenantId = job.SysTenantId,
                           OAJobDutyId = job.OAJobDutyId,
                           OAJobLevelId = job.OAJobLevelId,
                           OAJobTypeId = job.OAJobTypeId,
                           IsEnabled = job.IsEnabled,
                           TeamName = (lfTeam == null ? "" : lfTeam.Name),
                           TypeName = (lfType == null ? "" : lfType.Name),
                           LevelName = (lfLevel == null ? "" : lfLevel.Name),
                           DutyName = (lfDuty == null ? "" : lfDuty.Name),
                       });
            return await sql.ToListAsync();
        }
    }
}
