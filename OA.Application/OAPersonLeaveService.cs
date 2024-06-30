using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OneForAll.Core;
using OA.Domain.Enums;
using OA.Domain.Interfaces;
using OA.Application.Dtos;
using OA.Domain.AggregateRoots;
using OA.Application.Interfaces;
using OA.Domain.Models;
using OA.Domain.Aggregates;
using OA.Domain.ValueObjects;
using OA.Domain;
using OneForAll.EFCore;
using OA.Domain.Repositorys;
using OneForAll.Core.Extension;

namespace OA.Application
{
    /// <summary>
    /// 人员资料
    /// </summary>
    public class OAPersonLeaveService : IOAPersonLeaveService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonLeaveManager _manager;
        private readonly IOAPersonManager _personManager;
        private readonly IOATeamMemberManager _memberManager;

        private readonly IOAPersonLeaveRepository _repository;
        private readonly IOATeamRepository _teamRepository;
        private readonly IOAPersonRepository _personRepository;
        private readonly IOATeamPersonContactRepository _memberRepository;
        private readonly IOATeamMemberHistoryRepository _historyRepository;
        public OAPersonLeaveService(
            IMapper mapper,
            IOAPersonLeaveManager manager,
            IOAPersonManager personManager,
            IOATeamMemberManager memberManager,
            IOATeamRepository teamRepository,
            IOAPersonLeaveRepository repository,
            IOAPersonRepository personRepository,
            IOATeamPersonContactRepository memberRepository,
            IOATeamMemberHistoryRepository historyRepository)
        {
            _mapper = mapper;
            _manager = manager;
            _personManager = personManager;
            _memberManager = memberManager;
            _repository = repository;
            _teamRepository = teamRepository;
            _personRepository = personRepository;
            _memberRepository = memberRepository;
            _historyRepository = historyRepository;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="creatorName">创建人</param>
        /// <param name="teamId">部门</param>
        /// <param name="startDate">开始入职日期</param>
        /// <param name="endDate">开始入职日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonLeaveDto>> GetListAsync(
             string name,
             string creatorName,
             Guid? teamId,
             DateTime? startDate,
             DateTime? endDate)
        {
            var data = await _manager.GetListAsync(name, creatorName, teamId, startDate, endDate);
            return _mapper.Map<IEnumerable<OAPersonLeaveAggr>, IEnumerable<OAPersonLeaveDto>>(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAPersonLeaveForm form)
        {
            return await _manager.AddAsync(form);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAPersonLeaveForm form)
        {
            return await _manager.UpdateAsync(form);
        }

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _manager.DeleteAsync(ids);
        }

        /// <summary>
        /// 确认离职
        /// </summary>
        /// <param name="id">数据id</param>
        /// <param name="form">人员信息表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> ConfirmAsync(Guid id, OAPersonLeaveConfirmForm form)
        {
            var data = await _repository.FindAsync(id);
            if (data == null)
                return BaseErrType.DataNotFound;

            // 1. 修改人员档案
            var errType = await _personManager.LeaveAsync(new List<Guid>() { form.PersonId });
            if (errType == BaseErrType.Success)
            {
                // 2. 移出团队
                var historys = new List<OATeamMemberHistory>();
                var members = await _memberRepository.GetListByPersonAsync(form.PersonId);

                if (members.Any())
                {
                    var memberIds = members.Select(s => s.Id).ToList();
                    var teamIds = members.Select(s => s.OATeam.Id).ToList();
                    var teams = await _teamRepository.GetListANTAsync(teamIds);
                    members.ForEach(e =>
                    {
                        var team = teams.FirstOrDefault(w => w.Id == e.OATeam.Id);
                        historys.Add(new OATeamMemberHistory()
                        {
                            OAPersonId = e.Id,
                            PersonName = e.Name,
                            PersonJob = e.Job,
                            OATeamId = team.Id,
                            TeamName = team.Name,
                            TargetOATeamId = team.Id,
                            TargetTeamName = team.Name,
                            Type = OATeamMemberTransferEnum.Leave,
                            Remark = $"办理离职：{data.CreatorName}，原因：{data.Reason}，备注：{data.Remark}"
                        });
                    });

                    errType = await _memberManager.DeleteAsync(memberIds);
                }

                // 3. 生成异动日志
                if (historys.Any())
                    await _historyRepository.AddRangeAsync(historys);
            }

            // 4. 删除登记
            if (errType == BaseErrType.Success)
            {
                return await _manager.ConfirmAsync(id);
            }

            return errType;
        }
    }
}
