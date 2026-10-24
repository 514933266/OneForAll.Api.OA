using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OneForAll.Core;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;
using OneForAll.Core.Extension;

namespace OA.Domain.Managers
{
    /// <summary>
    /// 部门类型
    /// </summary>
    public class OATeamTypeManager : OADomainManager, IOATeamTypeManager
    {
        private readonly IMapper _mapper;
        private readonly IOATeamTypeRepository _repository;
        public OATeamTypeManager(
            IMapper mapper,
            ICurrentUser currentUser,
            IOATeamTypeRepository repository) : base(currentUser)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OATeamTypeForm entity)
        {
            var exists = await _repository.GetByNameExactAsync(entity.Name);
            if (exists != null) return BaseErrType.DataExist;

            var data = _mapper.Map<OATeamType>(entity);
            data.SysTenantId = LoginUser.TenantId.TryGuid();

            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OATeamTypeForm entity)
        {
            var exists = await _repository.GetByNameExactAsync(entity.Name);
            if (exists != null && exists.Id != entity.Id) return BaseErrType.DataExist;

            var data = await _repository.FindAsync(entity.Id);
            if (data == null) return BaseErrType.DataNotFound;

            data.Name = entity.Name;
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">团队组织id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid id)
        {
            var data = await _repository.FindAsync(id);
            if (data == null) return BaseErrType.DataNotFound;

            return await ResultAsync(() => _repository.DeleteAsync(data));
        }
    }
}
