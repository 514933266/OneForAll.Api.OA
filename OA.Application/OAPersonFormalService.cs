using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OA.Domain.Repositorys;
using OA.Domain.ValueObjects;
using OneForAll.Core;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 转正管理
    /// </summary>
    public class OAPersonFormalService : IOAPersonFormalService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonFormalManager _manager;
        private readonly IOATeamMemberHistoryManager _memberHistoryManager;

        private readonly IOATeamPersonContactRepository _memberRepository;
        public OAPersonFormalService(
            IMapper mapper,
            IOAPersonFormalManager manager,
            IOATeamMemberHistoryManager memberHistoryManager,
            IOATeamPersonContactRepository memberRepository)
        {
            _mapper = mapper;
            _manager = manager;
            _memberHistoryManager = memberHistoryManager;
            _memberRepository = memberRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="name">员工姓名</param>
        /// <param name="teamId">团队id</param>
        /// <param name="planStartDate">计划转正-开始日期</param>
        /// <param name="planEndDate">计划转正-开始日期</param>
        /// <param name="actualStartDate">实际转正-开始日期</param>
        /// <param name="actualEndDate">实际转正-开始日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonFormalDto>> GetListAsync(
            string name,
            Guid teamId,
            DateTime? planStartDate,
            DateTime? planEndDate,
            DateTime? actualStartDate,
            DateTime? actualEndDate)
        {
            var data = await _manager.GetListAsync(name, teamId, planStartDate, planEndDate, actualStartDate, actualEndDate);
            return _mapper.Map<IEnumerable<OAPersonFormalAggr>, IEnumerable<OAPersonFormalDto>>(data);
        }

        /// <summary>
        /// 办理转正
        /// </summary>
        /// <param name="id">数据id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> ConfirmAsync(Guid id, OAPersonFormalConfirmForm form)
        {
            var errType = await _manager.ConfirmAsync(id, form);

            if (errType == BaseErrType.Success)
            {
                var members = await _memberRepository.GetListByPersonAsync(form.Id);
                var member = members.FirstOrDefault(w => w.OATeam != null);
                if (member != null)
                {
                    var errType2 = await _memberHistoryManager.AddAsync(new OATeamMemberForm()
                    {
                        TeamId = member.OATeam.Id,
                        Id = member.Id,
                        Name = member.Name,
                        Job = member.Job,
                        IdCard = member.IdCard,
                        Remark = "办理转正"
                    });
                }
            }
            return errType;
        }
    }
}
