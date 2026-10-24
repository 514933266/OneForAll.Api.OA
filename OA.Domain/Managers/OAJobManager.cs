using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OA.Domain.Enums;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Aggregates;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Managers
{
    /// <summary>
    /// 职位管理
    /// </summary>
    public class OAJobManager : OADomainManager, IOAJobManager
    {
        private readonly IMapper _mapper;
        private readonly IOAJobRepository _repository;

        public OAJobManager(
            IMapper mapper,
            ICurrentUser currentUser,
            IOAJobRepository repository) : base(currentUser)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAJobForm form)
        {
            var exists = await _repository.GetByNameAsync(form.Name);
            if (exists != null) return BaseErrType.DataExist;

            var data = _mapper.Map<OAJob>(form);
            data.SysTenantId = LoginUser.TenantId.TryGuid();
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAJobForm form)
        {
            var exists = await _repository.GetByNameAsync(form.Name);
            if (exists != null && exists.Id != form.Id) return BaseErrType.DataExist;

            var data = await _repository.FindAsync(form.Id);
            if (data == null) return BaseErrType.DataNotFound;

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
            if (!data.Any()) return BaseErrType.DataEmpty;
            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 启用/停用
        /// </summary>
        /// <param name="id">菜单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> SetIsEnabledAsync(Guid id)
        {
            var data = await _repository.FindAsync(id);
            if (data == null)
                return BaseErrType.DataNotFound;

            data.SetIsEnabled();

            return await ResultAsync(() => _repository.UpdateAsync(data));
        }
    }
}
