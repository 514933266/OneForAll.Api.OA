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
    /// 员工入职
    /// </summary>
    public class OAPersonEntryRepository : Repository<OAPersonEntry>, IOAPersonEntryRepository
    {
        public OAPersonEntryRepository(DbContext context)
            : base(context)
        {
        }

        #region 列表

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<OAPersonEntry>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<OAPersonEntry>(w => true);
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

            return new PageList<OAPersonEntry>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="creatorName">创建人</param>
        /// <param name="mobilePhone">手机号码</param>
        /// <param name="startDate">开始入职日期</param>
        /// <param name="endDate">开始入职日期</param>
        /// <returns></returns>
        public async Task<IEnumerable<OAPersonEntryAggr>> GetListAsync(
            string name,
            string creatorName,
            string mobilePhone,
            DateTime? startDate,
            DateTime? endDate)
        {
            var predicate = PredicateBuilder.Create<OAPersonEntry>(w => true);
            if (!name.IsNullOrEmpty())
                predicate = predicate.And(w => w.Name.Contains(name));
            if (!creatorName.IsNullOrEmpty())
                predicate = predicate.And(w => w.CreatorName.Contains(creatorName));
            if (!mobilePhone.IsNullOrEmpty())
                predicate = predicate.And(w => w.MobilePhone.Contains(mobilePhone));
            if (startDate != null)
                predicate = predicate.And(w => w.EstimateEntryDate >= startDate);
            if (endDate != null)
                predicate = predicate.And(w => w.EstimateEntryDate <= endDate);

            var dbSet = DbSet.Where(predicate);
            var teamDbSet = Context.Set<OATeam>();
            var sql = (from entry in dbSet
                       join team in teamDbSet on entry.TeamId equals team.Id
                       select new OAPersonEntryAggr()
                       {
                           Id = entry.Id,
                           Name = entry.Name,
                           Job = entry.Job,
                           TeamId = entry.TeamId,
                           MobilePhone = entry.MobilePhone,
                           SysTenantId = entry.SysTenantId,
                           CreatorId = entry.CreatorId,
                           CreatorName = entry.CreatorName,
                           CreateTime = entry.CreateTime,
                           EstimateEntryDate = entry.EstimateEntryDate,
                           IsSubmitEntryFile = entry.IsSubmitEntryFile,
                           ExtendInformationJson = entry.ExtendInformationJson,
                           OATeam = team
                       });
            return await sql.ToListAsync();
        }
        #endregion

        #region 单体

        /// <summary>
        /// 查询指定id数据
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        public async Task<OAPersonEntry> GetIQFAsync(Guid id)
        {
            return await DbSet.IgnoreQueryFilters().FirstOrDefaultAsync(w => w.Id == id);
        }

        #endregion
    }
}
