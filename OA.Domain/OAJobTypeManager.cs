using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OA.Domain.Enums;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Aggregates;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;

namespace OA.Domain
{
    /// <summary>
    /// 职级类型
    /// </summary>
    public class OAJobTypeManager : OABaseManager, IOAJobTypeManager
    {
        private readonly IOAJobTypeRepository _repository;

        public OAJobTypeManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOAJobTypeRepository repository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<OAJobType>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            return await _repository.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAJobType>> GetListAsync(string key)
        {
            if (key.IsNullOrEmpty())
            {
                return await _repository.GetListAsync();
            }
            else
            {
                return await _repository.GetListAsync(w => w.Name.Contains(key));
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAJobTypeForm form)
        {
            var exists = await _repository.GetAsync(w => w.Name == form.Name);
            if (exists != null) return BaseErrType.DataExist;

            var data = _mapper.Map<OAJobType>(form);
            data.SysTenantId = LoginUser.SysTenantId;
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAJobTypeForm form)
        {
            var exists = await _repository.GetAsync(w => w.Name == form.Name);
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
            var data = await _repository.GetListAsync(w => ids.Contains(w.Id));
            if (!data.Any()) return BaseErrType.DataEmpty;
            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }
    }
}
