using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OneForAll.Core;
using OA.Domain.Interfaces;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.Models;
using OA.Domain.Aggregates;

namespace OA.Application
{
    /// <summary>
    /// 人员资料
    /// </summary>
    public class OAPersonLeaveService : IOAPersonLeaveService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonLeaveManager _manager;
        private readonly IOAPersonLeaveQueryService _leaveQueryService;
        private readonly IOAPersonManager _personManager;
        private readonly IOATeamMemberManager _memberManager;
        private readonly IOATeamMemberQueryService _memberQueryService;
        private readonly IOATeamMemberHistoryManager _memberHistoryManager;

        public OAPersonLeaveService(
            IMapper mapper,
            IOAPersonLeaveManager manager,
            IOAPersonLeaveQueryService leaveQueryService,
            IOAPersonManager personManager,
            IOATeamMemberManager memberManager,
            IOATeamMemberQueryService memberQueryService,
            IOATeamMemberHistoryManager memberHistoryManager)
        {
            _mapper = mapper;
            _manager = manager;
            _leaveQueryService = leaveQueryService;
            _personManager = personManager;
            _memberManager = memberManager;
            _memberQueryService = memberQueryService;
            _memberHistoryManager = memberHistoryManager;
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
            var data = await _leaveQueryService.GetListAsync(name, creatorName, teamId, startDate, endDate);
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
            var data = await _leaveQueryService.GetAsync(id);
            if (data == null)
                return BaseErrType.DataNotFound;

            // 1. 修改人员档案
            var errType = await _personManager.LeaveAsync(new List<Guid>() { form.PersonId });
            if (errType == BaseErrType.Success)
            {
                var members = await _memberQueryService.GetListByPersonAsync(form.PersonId);
                if (members.Any())
                {
                    var memberIds = members.Select(s => s.Id).ToList();

                    // 2. 生成异动日志
                    await _memberHistoryManager.AddRangeOnLeaveAsync(members, data);

                    // 3. 移出团队
                    errType = await _memberManager.DeleteAsync(memberIds);
                }
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
