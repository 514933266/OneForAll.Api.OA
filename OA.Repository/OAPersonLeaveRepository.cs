using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    /// 离职登记
    /// </summary>
    public class OAPersonLeaveRepository : Repository<OAPersonLeave>, IOAPersonLeaveRepository
    {
        public OAPersonLeaveRepository(DbContext context)
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
        public async Task<PageList<OAPersonLeave>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<OAPersonLeave>(w => true);
            var total = await DbSet.CountAsync(predicate);

            var data = await DbSet
                .Where(predicate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageList<OAPersonLeave>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="creatorName">创建人</param>
        /// <param name="teamId">手机号码</param>
        /// <param name="startDate">开始入职日期</param>
        /// <param name="endDate">开始入职日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonLeaveAggr>> GetListAsync(
             string name,
             string creatorName,
             Guid? teamId,
             DateTime? startDate,
             DateTime? endDate)
        {
            var predicate = PredicateBuilder.Create<OAPersonLeave>(w => true);
            if (!creatorName.IsNullOrEmpty())
                predicate = predicate.And(w => w.CreatorName.Contains(creatorName));
            if (startDate != null)
                predicate = predicate.And(w => w.EstimateLeaveDate >= startDate);
            if (endDate != null)
                predicate = predicate.And(w => w.EstimateLeaveDate <= endDate);

            var dbSet = DbSet.Where(predicate);
            var personSet = name.IsNullOrEmpty() ? Context.Set<OAPerson>() : Context.Set<OAPerson>().Where(w => w.Name.Contains(name));
            var memberSet = Context.Set<OATeamPersonContact>();
            var teamDbSet = teamId == null ? Context.Set<OATeam>() : Context.Set<OATeam>().Where(w => w.Id == teamId);
            var sql = (from leave in dbSet
                       join person in personSet on leave.OAPersonId equals person.Id
                       join member in memberSet on person.Id equals member.OAPersonId into letJoinMember
                       from lfMember in letJoinMember.DefaultIfEmpty()
                       join team in teamDbSet on lfMember.OATeamId equals team.Id into letJoinTeam
                       from lfTeam in letJoinTeam.DefaultIfEmpty()
                       select new OAPersonLeaveAggr()
                       {
                           Id = leave.Id,
                           Reason = leave.Reason,
                           Remark = leave.Remark,
                           SysTenantId = leave.SysTenantId,
                           CreatorId = leave.CreatorId,
                           CreatorName = leave.CreatorName,
                           CreateTime = leave.CreateTime,
                           CanCreateHistory = leave.CanCreateHistory,
                           EstimateLeaveDate = leave.EstimateLeaveDate,
                           OATeam = lfTeam,
                           OAPerson = person
                       });
            return await sql.ToListAsync();
        }
    }
}
