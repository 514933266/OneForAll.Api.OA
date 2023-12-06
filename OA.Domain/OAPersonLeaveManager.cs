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
    /// 离职登记
    /// </summary>
    public class OAPersonLeaveManager : OABaseManager, IOAPersonLeaveManager
    {
        private readonly IOAPersonLeaveRepository _repository;

        public OAPersonLeaveManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOAPersonLeaveRepository repository) : base(mapper, httpContextAccessor)
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
        public async Task<PageList<OAPersonLeave>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            return await _repository.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="creatorName">创建人</param>
        /// <param name="teamId">手机号码</param>
        /// <param name="startDate">开始入职日期</param>
        /// <param name="endDate">开始入职日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonLeaveAggr>> GetListAsync(
             string name,
             string creatorName,
             Guid? teamId,
             DateTime? startDate,
             DateTime? endDate)
        {
            return await _repository.GetListAsync(name, creatorName, teamId, startDate, endDate);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAPersonLeaveForm form)
        {
            var exists = await _repository.GetAsync(w => w.OAPersonId == form.PersonId);
            if (exists != null)
                return BaseErrType.DataExist;

            var data = _mapper.Map<OAPersonLeaveForm, OAPersonLeave>(form);
            data.SysTenantId = LoginUser.SysTenantId;
            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;
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
            var data = await _repository.GetListAsync(w => ids.Contains(w.Id));
            if (!data.Any())
                return BaseErrType.DataEmpty;
            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 确认到岗
        /// </summary>
        /// <param name="id">数据id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> ConfirmAsync(Guid id)
        {
            return await DeleteAsync(new List<Guid>() { id });
        }
    }
}
