using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OneForAll.Core;
using OA.Domain.Models;
using OA.Application.Dtos;
using OA.Domain.Interfaces;
using OA.Application.Interfaces;
using OneForAll.Core.Extension;
using System.IO;

namespace OA.Application
{
    /// <summary>
    /// 团队成员
    /// </summary>
    public class OATeamMemberService : IOATeamMemberService
    {
        private readonly IMapper _mapper;
        private readonly IOATeamMemberManager _manager;
        private readonly IOAPersonManager _personManager;
        private readonly IOATeamMemberHistoryManager _historyManager;

        public OATeamMemberService(
            IMapper mapper,
            IOATeamMemberManager manager,
            IOAPersonManager personManager,
            IOATeamMemberHistoryManager historyManager)
        {
            _mapper = mapper;
            _manager = manager;
            _personManager = personManager;
            _historyManager = historyManager;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="teamId">团队id，当为默认值的时候返回没有团队的人员信息</param>
        /// <param name="deep">是否递归</param>
        /// <returns>结果</returns>
        public async Task<IEnumerable<OATeamMemberDto>> GetListAsync(Guid teamId, bool deep)
        {
            var data = await _manager.GetListAsync(teamId, deep);
            return _mapper.Map<IEnumerable<OATeamMemberDto>>(data);
        }

        /// <summary>
        /// 获取未加入团队列表
        /// </summary>
        /// <returns>成员列表</returns>
        public async Task<IEnumerable<OANoTeamPersonDto>> GetListNoTeamAsync()
        {
            var data = await _manager.GetListNoTeamAsync();
            return _mapper.Map<IEnumerable<OANoTeamPersonDto>>(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">成员信息</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OATeamMemberForm form)
        {
            var person = _mapper.Map<OAPersonForm>(form);

            var result = await _personManager.AddAsync(person);
            if (result == BaseErrType.Success || result == BaseErrType.DataExist)
            {
                result = await _manager.AddAsync(form);
                if (result == BaseErrType.Success)
                {
                    await _historyManager.AddAsync(form);
                }
            }
            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">成员信息</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OATeamMemberForm form)
        {
            var result = await _personManager.UpdateAsync(form);
            if (result == BaseErrType.Success)
            {
                return await _manager.UpdateAsync(form);
            }
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(OATeamMemberDeleteForm form)
        {
            if (form.IsLeave)
            {
                await _historyManager.AddAsync(form);
                var errType = await _personManager.LeaveAsync(form.Ids);
                if (errType == BaseErrType.Success)
                {
                    errType = await _manager.LeaveAsync(form.TeamId, form.Ids);
                }
                return errType;
            }
            else
            {
                return await _manager.DeleteAsync(form.TeamId, form.Ids);
            }
        }

        /// <summary>
        /// 调动部门
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> TransferAsync(OATeamMemberTransferForm form)
        {
            await _historyManager.AddAsync(form);
            return await _manager.TransferAsync(form.TeamId, form.TargetTeamId, form.Ids);
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="teamId">部门id</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public async Task<BaseMessage> ImportExcelAsync(Guid teamId, IEnumerable<OATeamMemberImportForm> data)
        {
            return await _manager.ImportExcelAsync(teamId, data);
        }
    }
}
