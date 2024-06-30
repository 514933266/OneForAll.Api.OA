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
using System.Linq.Expressions;

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
        /// 创建系统默认职位分类
        /// </summary>
        /// <returns></returns>
        public async Task<BaseErrType> CreateDefaultAsync()
        {
            var data = new List<OAJobType>() {
                new OAJobType() { SysTenantId = LoginUser.SysTenantId, Name = "行政管理类", Remark = "包括行政总监、行政经理、行政助理等" },
                new OAJobType() { SysTenantId = LoginUser.SysTenantId, Name = "人力资源类", Remark = "包括人力资源总监、人力资源经理、招聘专员等" },
                new OAJobType() { SysTenantId = LoginUser.SysTenantId, Name = "财务会计类", Remark = "包括财务总监、财务经理、会计师等" },
                new OAJobType() { SysTenantId = LoginUser.SysTenantId, Name = "销售市场类", Remark = "包括销售总监、销售经理、市场营销专员等" },
                new OAJobType() { SysTenantId = LoginUser.SysTenantId, Name = "技术研发类", Remark = "包括技术总监、研发工程师、软件开发工程师等" },
                new OAJobType() { SysTenantId = LoginUser.SysTenantId, Name = "客户服务类", Remark = "包括客户服务经理、客户关系管理专员等" }
            };
            return await ResultAsync(() => _repository.AddRangeAsync(data));
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
