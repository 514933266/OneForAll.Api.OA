using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using OneForAll.Core;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;
using OneForAll.Core.Extension;

namespace OA.Domain
{
    /// <summary>
    /// 部门类型
    /// </summary>
    public class OATeamTypeManager : OABaseManager, IOATeamTypeManager
    {
        private readonly IOATeamTypeRepository _repository;
        public OATeamTypeManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOATeamTypeRepository repository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamType>> GetListAsync(string name)
        {
            if (!name.IsNullOrEmpty())
            {
                return await _repository.GetListAsync(name);
            }
            else
            {
                return await _repository.GetListAsync();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OATeamTypeForm entity)
        {
            var exists = await _repository.GetAsync(w => w.Name == entity.Name);
            if (exists != null) return BaseErrType.DataExist;

            var data = _mapper.Map<OATeamType>(entity);
            data.SysTenantId = LoginUser.SysTenantId;

            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OATeamTypeForm entity)
        {
            var exists = await _repository.GetAsync(w => w.Name == entity.Name);
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
