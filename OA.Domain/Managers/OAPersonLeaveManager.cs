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
using OA.Domain.Aggregates;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Managers
{
    /// <summary>
    /// 离职登记
    /// </summary>
    public class OAPersonLeaveManager : OADomainManager, IOAPersonLeaveManager
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonLeaveRepository _repository;
        private readonly IOATeamPersonContactRepository _contactRepository;

        public OAPersonLeaveManager(
            IMapper mapper,
            ICurrentUser currentUser,
            IOAPersonLeaveRepository repository,
            IOATeamPersonContactRepository contactRepository) : base(currentUser)
        {
            _mapper = mapper;
            _repository = repository;
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAPersonLeaveForm form)
        {
            var exists = await _repository.GetByPersonIdAsync(form.PersonId);
            if (exists != null)
                return BaseErrType.DataExist;

            var members = await _contactRepository.GetListByPersonAsync(form.PersonId);

            var data = _mapper.Map<OAPersonLeaveForm, OAPersonLeave>(form);
            data.TeamName = string.Join(',', members.Select(s => s.OATeam?.Name));
            data.SysTenantId = LoginUser.TenantId.TryGuid().TryGuid();
            data.CreatorId = LoginUser.Id.TryGuid().TryGuid();
            data.CreatorName = LoginUser.DisplayName;
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAPersonLeaveForm form)
        {
            var data = await _repository.FindAsync(form.Id);
            if (data == null) 
                return BaseErrType.DataNotFound;

            _mapper.Map(form, data);
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">项目id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            var data = await _repository.GetListByIdsAsync(ids);
            if (!data.Any())
                return BaseErrType.DataEmpty;
            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 确认离职
        /// </summary>
        /// <param name="id">数据id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> ConfirmAsync(Guid id)
        {
            return await DeleteAsync(new List<Guid>() { id });
        }
    }
}
