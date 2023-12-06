using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using OneForAll.Core.ORM;
using OA.Domain.Repositorys;
using OneForAll.Core.Extension;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.ValueObjects;

namespace OA.Repository
{
    /// <summary>
    /// 团队组织人员
    /// </summary>
    public class OATeamPersonContactRepository : Repository<OATeamPersonContact>, IOATeamPersonContactRepository
    {
        public OATeamPersonContactRepository(DbContext context)
           : base(context)
        {

        }

        /// <summary>
        /// 查询列表（含人员基础信息）
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamMemberAggr>> GetListByTeamAsync(Guid teamId)
        {
            var predicate = PredicateBuilder.Create<OATeamPersonContact>(w => true);
            if (teamId != Guid.Empty)
                predicate = predicate.And(w => w.OATeamId == teamId);

            var personDbSet = Context.Set<OAPerson>();
            var teamDbSet = Context.Set<OATeam>();

            var sql = (from contact in DbSet.Where(predicate)
                       join person in personDbSet on contact.OAPersonId equals person.Id
                       join team in teamDbSet on contact.OATeamId equals team.Id
                       orderby contact.IsLeader descending, contact.CreateTime descending
                       select new OATeamMemberAggr()
                       {
                           Id = person.Id,
                           Name = person.Name,
                           Sex = person.Sex,
                           Age = person.Age,
                           Job = person.Job,
                           Email = person.Email,
                           IdCard = person.IdCard,
                           Remark = person.Remark,
                           IconUrl = person.IconUrl,
                           JoinAge = person.JoinAge,
                           Birthday = person.Birthday,
                           EntryDate = person.EntryDate,
                           LeaveDate = person.LeaveDate,
                           WorkNumber = person.WorkNumber,
                           MobilePhone = person.MobilePhone,
                           SysTenantId = person.SysTenantId,
                           EmployeeType = person.EmployeeType,
                           EmployeeStatus = person.EmployeeStatus,
                           ExtendInformationJson = person.ExtendInformationJson,
                           OATeam = team,
                           Contact = contact
                       });

            return await sql.ToListAsync();
        }

        /// <summary>
        /// 查询列表（含人员基础信息）
        /// </summary>
        /// <param name="teamIds">团队id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamMemberAggr>> GetListByTeamAsync(IEnumerable<Guid> teamIds)
        {
            var predicate = PredicateBuilder.Create<OATeamPersonContact>(w => teamIds.Contains(w.OATeamId));

            var personDbSet = Context.Set<OAPerson>();
            var teamDbSet = Context.Set<OATeam>();

            var sql = (from contact in DbSet.Where(predicate)
                       join person in personDbSet on contact.OAPersonId equals person.Id
                       join team in teamDbSet on contact.OATeamId equals team.Id
                       orderby contact.IsLeader descending, contact.CreateTime descending
                       select new OATeamMemberAggr()
                       {
                           Id = person.Id,
                           Name = person.Name,
                           Sex = person.Sex,
                           Age = person.Age,
                           Job = person.Job,
                           Email = person.Email,
                           IdCard = person.IdCard,
                           Remark = person.Remark,
                           IconUrl = person.IconUrl,
                           JoinAge = person.JoinAge,
                           Birthday = person.Birthday,
                           EntryDate = person.EntryDate,
                           LeaveDate = person.LeaveDate,
                           WorkNumber = person.WorkNumber,
                           MobilePhone = person.MobilePhone,
                           SysTenantId = person.SysTenantId,
                           EmployeeType = person.EmployeeType,
                           EmployeeStatus = person.EmployeeStatus,
                           ExtendInformationJson = person.ExtendInformationJson,
                           OATeam = team,
                           Contact = contact
                       });

            return await sql.ToListAsync();
        }

        /// <summary>
        /// 查询列表（含人员基础信息）
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="personIds">实体id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamMemberAggr>> GetListByTeamAsync(Guid teamId, IEnumerable<Guid> personIds)
        {
            var predicate = PredicateBuilder.Create<OATeamPersonContact>(w => personIds.Contains(w.OAPersonId));
            if (teamId != Guid.Empty)
                predicate = predicate.And(w => w.OATeamId == teamId);

            var personDbSet = Context.Set<OAPerson>();
            var teamDbSet = Context.Set<OATeam>();

            var sql = (from contact in DbSet.Where(predicate)
                       join person in personDbSet on contact.OAPersonId equals person.Id
                       join team in teamDbSet on contact.OATeamId equals team.Id
                       orderby contact.IsLeader descending, contact.CreateTime descending
                       select new OATeamMemberAggr()
                       {
                           Id = person.Id,
                           Name = person.Name,
                           Sex = person.Sex,
                           Age = person.Age,
                           Job = person.Job,
                           Email = person.Email,
                           IdCard = person.IdCard,
                           Remark = person.Remark,
                           IconUrl = person.IconUrl,
                           JoinAge = person.JoinAge,
                           Birthday = person.Birthday,
                           EntryDate = person.EntryDate,
                           LeaveDate = person.LeaveDate,
                           WorkNumber = person.WorkNumber,
                           MobilePhone = person.MobilePhone,
                           SysTenantId = person.SysTenantId,
                           EmployeeType = person.EmployeeType,
                           EmployeeStatus = person.EmployeeStatus,
                           ExtendInformationJson = person.ExtendInformationJson,
                           OATeam = team,
                           Contact = contact
                       });

            return await sql.ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="name">员工姓名</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamMemberAggr>> GetListByTeamAsync(Guid teamId, string name)
        {
            var predicate = PredicateBuilder.Create<OATeamPersonContact>(w => true);
            if (teamId != Guid.Empty)
                predicate = predicate.And(w => w.OATeamId == teamId);

            var personDbSet = name.IsNullOrEmpty() ? Context.Set<OAPerson>() : Context.Set<OAPerson>().Where(w => w.Name.Contains(name));
            var teamDbSet = Context.Set<OATeam>();

            var sql = (from contact in DbSet.Where(predicate)
                       join person in personDbSet on contact.OAPersonId equals person.Id
                       join team in teamDbSet on contact.OATeamId equals team.Id
                       orderby contact.IsLeader descending, contact.CreateTime descending
                       select new OATeamMemberAggr()
                       {
                           Id = person.Id,
                           Name = person.Name,
                           Sex = person.Sex,
                           Age = person.Age,
                           Job = person.Job,
                           Email = person.Email,
                           IdCard = person.IdCard,
                           Remark = person.Remark,
                           IconUrl = person.IconUrl,
                           JoinAge = person.JoinAge,
                           Birthday = person.Birthday,
                           EntryDate = person.EntryDate,
                           LeaveDate = person.LeaveDate,
                           WorkNumber = person.WorkNumber,
                           MobilePhone = person.MobilePhone,
                           SysTenantId = person.SysTenantId,
                           EmployeeType = person.EmployeeType,
                           EmployeeStatus = person.EmployeeStatus,
                           ExtendInformationJson = person.ExtendInformationJson,
                           OATeam = team,
                           Contact = contact
                       });

            return await sql.ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="personId">人员档案id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamMemberAggr>> GetListByPersonAsync(Guid personId)
        {
            var predicate = PredicateBuilder.Create<OATeamPersonContact>(w => w.OAPersonId == personId);


            var personDbSet = Context.Set<OAPerson>();
            var teamDbSet = Context.Set<OATeam>();

            var sql = (from contact in DbSet.Where(predicate)
                       join person in personDbSet on contact.OAPersonId equals person.Id
                       join team in teamDbSet on contact.OATeamId equals team.Id
                       orderby contact.IsLeader descending, contact.CreateTime descending
                       select new OATeamMemberAggr()
                       {
                           Id = person.Id,
                           Name = person.Name,
                           Sex = person.Sex,
                           Age = person.Age,
                           Job = person.Job,
                           Email = person.Email,
                           IdCard = person.IdCard,
                           Remark = person.Remark,
                           IconUrl = person.IconUrl,
                           JoinAge = person.JoinAge,
                           Birthday = person.Birthday,
                           EntryDate = person.EntryDate,
                           LeaveDate = person.LeaveDate,
                           WorkNumber = person.WorkNumber,
                           MobilePhone = person.MobilePhone,
                           SysTenantId = person.SysTenantId,
                           EmployeeType = person.EmployeeType,
                           EmployeeStatus = person.EmployeeStatus,
                           ExtendInformationJson = person.ExtendInformationJson,
                           OATeam = team,
                           Contact = contact
                       });

            return await sql.ToListAsync();
        }

        /// <summary>
        /// 查询团队成员数量
        /// </summary>
        /// <param name="teamIds">团队id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamMemberCountVo>> GetListCountByTeamAsync(IEnumerable<Guid> teamIds)
        {
            var sql = (from contact in DbSet.Where(w => teamIds.Contains(w.OATeamId))
                       group contact by contact.OATeamId into gData
                       select new OATeamMemberCountVo()
                       {
                           TeamId = gData.Key,
                           Count = gData.Count()
                       });

            return await sql.ToListAsync();
        }

        /// <summary>
        /// 查询团队成员数量
        /// </summary>
        /// <param name="teamIds">团队id</param>
        /// <returns>列表</returns>
        public async Task<int> GetCountByTeamAsync(IEnumerable<Guid> teamIds)
        {
            var sql = (from contact in DbSet.Where(w => teamIds.Contains(w.OATeamId))
                       select contact.Id);

            return await sql.CountAsync();
        }

        /// <summary>
        /// 查询当前用户所在的团队
        /// </summary>
        /// <param name="loginUserId">当前登录用户id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamPersonContact>> GetListByUserAsync(Guid loginUserId)
        {
            var personDbSet = Context.Set<OAPerson>().Where(w => w.SysUserId == loginUserId);

            var sql = (from contact in DbSet
                       join person in personDbSet on contact.OAPersonId equals person.Id
                       select contact);

            return await sql.ToListAsync();
        }
    }
}
