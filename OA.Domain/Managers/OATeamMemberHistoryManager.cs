using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.Aggregates;
using OA.Domain.AggregateRoots;
using OA.Domain.Enums;

namespace OA.Domain.Managers
{
    /// <summary>
    /// 团队成员异动历史
    /// </summary>
    public class OATeamMemberHistoryManager : OADomainManager, IOATeamMemberHistoryManager
    {
        private readonly IMapper _mapper;
        private readonly IOATeamRepository _teamRepository;
        private readonly IOAPersonRepository _personRepository;
        private readonly IOATeamMemberHistoryRepository _repository;
        private readonly IOATeamPersonContactRepository _contactRepository;

        public OATeamMemberHistoryManager(
            IMapper mapper,
            ICurrentUser currentUser,
            IOATeamRepository teamRepository,
            IOAPersonRepository personRepository,
            IOATeamMemberHistoryRepository repository,
            IOATeamPersonContactRepository contactRepository) : base(currentUser)
        {
            _mapper = mapper;
            _repository = repository;
            _teamRepository = teamRepository;
            _contactRepository = contactRepository;
            _personRepository = personRepository;
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
                SysTenantId = LoginUser.TenantId.TryGuid(),
                OAPersonId = person.Id,
                PersonJob = person.Job,
                PersonName = person.Name,
                TargetOATeamId = team.Id,
                TargetTeamName = team.Name,
                CreatorId = LoginUser.Id.TryGuid(),
                CreatorName = LoginUser.DisplayName,
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
                    SysTenantId = LoginUser.TenantId.TryGuid(),
                    OAPersonId = e.Id,
                    PersonJob = e.Job,
                    PersonName = e.Name,
                    OATeamId = team.Id,
                    TeamName = team.Name,
                    TargetOATeamId = targetTeam.Id,
                    TargetTeamName = targetTeam.Name,
                    Type = OATeamMemberTransferEnum.Normal,
                    CreatorId = LoginUser.Id.TryGuid(),
                    CreatorName = LoginUser.DisplayName,
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
                    SysTenantId = LoginUser.TenantId.TryGuid(),
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

        /// <summary>
        /// 办理离职时批量生成异动日志
        /// </summary>
        /// <param name="members">成员列表（含团队）</param>
        /// <param name="leave">离职登记</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddRangeOnLeaveAsync(IEnumerable<OATeamMemberAggr> members, OAPersonLeave leave)
        {
            if (members == null || !members.Any())
                return BaseErrType.Success;

            var teamIds = members.Select(s => s.OATeam.Id).ToList();
            var teams = await _teamRepository.GetListANTAsync(teamIds);

            var remark = $"办理离职：{leave.CreatorName}，原因：{leave.Reason}，备注：{leave.Remark}";
            var data = new List<OATeamMemberHistory>();
            members.ForEach(e =>
            {
                var team = teams.FirstOrDefault(w => w.Id == e.OATeam.Id);
                if (team == null) return;

                data.Add(new OATeamMemberHistory()
                {
                    OAPersonId = e.Id,
                    PersonName = e.Name,
                    PersonJob = e.Job,
                    OATeamId = team.Id,
                    TeamName = team.Name,
                    TargetOATeamId = team.Id,
                    TargetTeamName = team.Name,
                    Type = OATeamMemberTransferEnum.Leave,
                    Remark = remark
                });
            });

            if (!data.Any())
                return BaseErrType.Success;

            return await ResultAsync(() => _repository.AddRangeAsync(data));
        }
    }
}
