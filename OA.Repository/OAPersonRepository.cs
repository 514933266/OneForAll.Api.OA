using Microsoft.EntityFrameworkCore;
using OA.Domain.AggregateRoots;
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
    /// 人员档案
    /// </summary>
    public class OAPersonRepository : Repository<OAPerson>, IOAPersonRepository
    {
        public OAPersonRepository(DbContext context)
            : base(context)
        {
        }

        #region 列表

        /// <summary>
        /// 查询人员分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<OAPerson>> GetPageAsync(int pageIndex, int pageSize, string key, OAPersonOnJobStatusEnum onJobStatus)
        {
            var predicate = PredicateBuilder.Create<OAPerson>(w => true);
            if (!key.IsNullOrEmpty())
            {
                predicate = predicate.And(w => w.Name.Contains(key));
            }
            switch (onJobStatus)
            {
                case OAPersonOnJobStatusEnum.OnJob: predicate = predicate.And(w => w.LeaveDate == null); break;
                case OAPersonOnJobStatusEnum.Leave: predicate = predicate.And(w => w.LeaveDate != null); break;
            }
            var total = await DbSet
                .CountAsync(predicate);

            var data = await DbSet
                .Where(predicate)
                .OrderByDescending(w => w.EntryDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageList<OAPerson>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPerson>> GetListAsync(string key, OAPersonOnJobStatusEnum onJobStatus)
        {
            var predicate = PredicateBuilder.Create<OAPerson>(w => true);
            if (!key.IsNullOrEmpty())
            {
                predicate = predicate.And(w => w.Name.Contains(key));
            }
            switch (onJobStatus)
            {
                case OAPersonOnJobStatusEnum.OnJob: predicate = predicate.And(w => w.LeaveDate == null); break;
                case OAPersonOnJobStatusEnum.Leave: predicate = predicate.And(w => w.LeaveDate != null); break;
            }

            return await DbSet
                .Where(predicate)
                .ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <param name="employeeStatus">员工状态</param>
        /// <param name="employeeType">员工类型</param>
        /// <param name="jobs">职级</param>
        /// <param name="startEntryDate">开始入职时间</param>
        /// <param name="endEntryDate">结束入职时间</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPerson>> GetListAsync(
            OAPersonOnJobStatusEnum onJobStatus,
            string employeeType,
            string employeeStatus,
            IEnumerable<string> jobs,
            DateTime? startEntryDate,
            DateTime? endEntryDate)
        {
            var predicate = PredicateBuilder.Create<OAPerson>(w => true);
            if (!employeeType.IsNullOrEmpty()) predicate = predicate.And(w => w.EmployeeType.Contains(employeeType));
            if (!employeeStatus.IsNullOrEmpty()) predicate = predicate.And(w => w.EmployeeStatus.Contains(employeeType));
            if (startEntryDate != null) predicate = predicate.And(w => w.EntryDate >= startEntryDate);
            if (endEntryDate != null) predicate = predicate.And(w => w.EntryDate <= endEntryDate);
            if (jobs.Any()) predicate = predicate.And(w => jobs.Contains(w.Job));

            switch (onJobStatus)
            {
                case OAPersonOnJobStatusEnum.OnJob: predicate = predicate.And(w => w.LeaveDate == null); break;
                case OAPersonOnJobStatusEnum.Leave: predicate = predicate.And(w => w.LeaveDate != null); break;
            }

            return await DbSet.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">人员id</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OAPerson>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await DbSet.Where(w => ids.Contains(w.Id)).ToListAsync();
        }

        /// <summary>
        /// 查询未加入团队列表
        /// </summary>
        /// <returns>结果</returns>
        public async Task<IEnumerable<OAPerson>> GetListNoTeamAsync()
        {
            var personQuery = Context.Set<OAPerson>().Where(w => w.LeaveDate == null);
            var teamContactQuery = Context.Set<OATeamPersonContact>().AsQueryable();

            var data = from person in personQuery
                       join teamContact in teamContactQuery
                            on person.Id equals teamContact.OAPersonId into leftJoinNoTeamPerson
                       from noTeamPerson in leftJoinNoTeamPerson.DefaultIfEmpty()
                       where noTeamPerson == null
                       select person;

            return await data.ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="idCards">身份证</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPerson>> GetListByIdCardAsync(IEnumerable<string> idCards)
        {
            if (idCards.Any())
            {
                var predicate = PredicateBuilder.Create<OAPerson>(w => idCards.Contains(w.IdCard));

                return await DbSet
                    .Where(predicate)
                    .ToListAsync();
            }
            else
            {
                return Enumerable.Empty<OAPerson>();
            }
        }

        /// <summary>
        /// 查询生日列表
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPerson>> GetListBirthdayAsync(DateTime startDate, DateTime endDate)
        {
            var predicate = PredicateBuilder.Create<OAPerson>(w => w.LeaveDate == null && w.Birthday.Value.Month >= startDate.Month && w.Birthday.Value.Month <= endDate.Month);

            return await DbSet
                .Where(predicate)
                .OrderBy(o => o.Birthday.Value.Month)
                .ThenBy(o => o.Birthday.Value.Day)
                .ToListAsync();
        }

        /// <summary>
        /// 查询入职周年列表
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPerson>> GetListCompanyAsync(DateTime date)
        {
            var predicate = PredicateBuilder.Create<OAPerson>(w => w.LeaveDate == null && w.EntryDate.Value.Month == date.Month && Microsoft.EntityFrameworkCore.SqlServerDbFunctionsExtensions.DateDiffYear(EF.Functions, w.EntryDate.Value, date) >= 1);

            return await DbSet
                .Where(predicate)
                .OrderByDescending(o => o.JoinAge)
                .ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPerson>> GetListByTenantId(Guid tenantId)
        {
            var predicate = PredicateBuilder.Create<OAPerson>(w => w.SysTenantId == tenantId);

            return await DbSet
                .IgnoreQueryFilters()
                .Where(predicate)
                .ToListAsync();
        }

        /// <summary>
        /// 查询新入职员工列表
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPerson>> GetListNewEntryAsync()
        {
            var predicate = PredicateBuilder.Create<OAPerson>(w => w.EntryDate >= DateTime.Now.AddMonths(-2));

            return await DbSet.Where(predicate).ToListAsync();
        }
        
        #endregion

        #region 实体

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>人员</returns>
        public async Task<OAPerson> GetIQFAsync(Guid id)
        {
            return await DbSet.IgnoreQueryFilters().Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查询人员
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="idCard">身份证号</param>
        /// <returns>人员</returns>
        public async Task<OAPerson> GetAsync(string name, string idCard)
        {
            return await DbSet.Where(w => w.Name.Equals(name) && w.IdCard == idCard).FirstOrDefaultAsync();
        }

        #endregion

    }
}
