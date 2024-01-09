using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;
using OA.Domain.Enums;
using static NPOI.HSSF.Util.HSSFColor;

namespace OA.Domain
{
    /// <summary>
    /// 团队成员异动历史
    /// </summary>
    public class OATeamMemberHistoryManager : OABaseManager, IOATeamMemberHistoryManager
    {

        private readonly IOATeamRepository _teamRepository;
        private readonly IOAPersonRepository _personRepository;
        private readonly IOATeamMemberHistoryRepository _repository;
        private readonly IOATeamPersonContactRepository _contactRepository;

        public OATeamMemberHistoryManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOATeamRepository teamRepository,
            IOAPersonRepository personRepository,
            IOATeamMemberHistoryRepository repository,
            IOATeamPersonContactRepository contactRepository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
            _teamRepository = teamRepository;
            _contactRepository = contactRepository;
            _personRepository = personRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="teamId"></param>
        /// <param name="key"></param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>结果</returns>
        public async Task<PageList<OATeamMemberHistory>> GetPageAsync(
            int pageIndex,
            int pageSize,
            Guid teamId,
            string key,
            DateTime? startDate,
            DateTime? endDate)
        {
            var data = await _repository.GetPageAsync(pageIndex, pageSize, teamId, key, startDate, endDate);
            return _mapper.Map<PageList<OATeamMemberHistory>>(data);
        }

        /// <summary>
        /// 添加入职历史
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OATeamMemberForm form)
        {
            var team = await _teamRepository.GetAsync(form.TeamId);
            if (team == null)
                return BaseErrType.DataError;

            var person = await _personRepository.GetAsync(form.Name, form.IdCard);
            if (person == null)
                return BaseErrType.DataError;

            var data = new OATeamMemberHistory()
            {
                SysTenantId = LoginUser.SysTenantId,
                OAPersonId = person.Id,
                PersonJob = person.Job,
                PersonName = person.Name,
                TargetOATeamId = team.Id,
                TargetTeamName = team.Name,
                CreatorId = LoginUser.Id,
                CreatorName = LoginUser.Name,
                Type = OATeamMemberTransferEnum.Join,
                Remark = (form.Remark.IsNullOrWhiteSpace() ? "入职" : form.Remark)
            };

            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 添加调动历史
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OATeamMemberTransferForm form)
        {
            var teamIds = new List<Guid>() { form.TeamId, form.TargetTeamId };

            var teams = await _teamRepository.GetListANTAsync(teamIds);
            var team = teams.FirstOrDefault(w => w.Id == form.TeamId);
            var targetTeam = teams.FirstOrDefault(w => w.Id == form.TargetTeamId);

            if (team == null)
                return BaseErrType.DataError;
            if (targetTeam == null)
                return BaseErrType.DataError;

            var contacts = await _contactRepository.GetListByTeamAsync(form.TeamId, form.Ids);
            if (!contacts.Any())
                return BaseErrType.DataNotFound;

            var data = new List<OATeamMemberHistory>();
            contacts.ForEach(e =>
            {
                data.Add(new OATeamMemberHistory()
                {
                    SysTenantId = LoginUser.SysTenantId,
                    OAPersonId = e.Id,
                    PersonJob = e.Job,
                    PersonName = e.Name,
                    OATeamId = team.Id,
                    TeamName = team.Name,
                    TargetOATeamId = targetTeam.Id,
                    TargetTeamName = targetTeam.Name,
                    Type = OATeamMemberTransferEnum.Normal,
                    CreatorId = LoginUser.Id,
                    CreatorName = LoginUser.Name,
                    Remark = (form.Remark.IsNullOrWhiteSpace() ? "调动" : form.Remark)
                });
            });
            return await ResultAsync(() => _repository.AddRangeAsync(data));
        }

        /// <summary>
        /// 添加离职历史
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OATeamMemberDeleteForm form)
        {
            var teamIds = new List<Guid>() { form.TeamId };

            var teams = await _teamRepository.GetListANTAsync(teamIds);
            var team = teams.FirstOrDefault(w => w.Id == form.TeamId);

            if (team == null)
                return BaseErrType.DataError;

            var contacts = await _contactRepository.GetListByTeamAsync(form.TeamId, form.Ids);
            if (!contacts.Any())
                return BaseErrType.DataNotFound;

            var data = new List<OATeamMemberHistory>();
            var str = form.IsLeave ? "离职" : "移出团队";
            contacts.ForEach(e =>
            {
                data.Add(new OATeamMemberHistory()
                {
                    SysTenantId = LoginUser.SysTenantId,
                    OAPersonId = e.Id,
                    PersonJob = e.Job,
                    PersonName = e.Name,
                    OATeamId = team.Id,
                    TeamName = team.Name,
                    Type = OATeamMemberTransferEnum.Leave,
                    Remark = (form.Remark.IsNullOrWhiteSpace() ? str : form.Remark)
                });
            });
            return await ResultAsync(() => _repository.AddRangeAsync(data));
        }
    }
}
