using AutoMapper;
using Microsoft.AspNetCore.Http;
using OA.Domain.AggregateRoots;
using OA.Domain.Enums;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OA.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain
{
    /// <summary>
    /// 自定义表格字段显示配置
    /// </summary>
    public class OATableColumnSettingManager : OABaseManager, IOATableColumnSettingManager
    {
        private readonly IOATableColumnSettingsRepository _repository;
        public OATableColumnSettingManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOATableColumnSettingsRepository repository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取个人配置
        /// </summary>
        /// <param name="target">目标类型</param>
        /// <returns>列表</returns>
        public async Task<OATableColumnSetting> GetAsync(OATableColumnSettingsTargetEnum target)
        {
            var data = await _repository.GetAsync(target, LoginUser.Id);
            if (data == null)
            {
                data = new OATableColumnSetting()
                {
                    Target = target,
                    CreatorId = LoginUser.Id,
                    SysTenantId = LoginUser.SysTenantId
                };
            }
            return data;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OATableColumnSettingForm entity)
        {
            var exists = await _repository.GetAsync(entity.Target, LoginUser.Id);
            if (exists != null) return BaseErrType.DataExist;

            var data = _mapper.Map<OATableColumnSetting>(entity);

            data.SysTenantId = LoginUser.SysTenantId;
            data.CreatorId = LoginUser.Id;

            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OATableColumnSettingForm entity)
        {
            var exists = await _repository.GetAsync(entity.Target, LoginUser.Id);
            if (exists != null && exists.Id != entity.Id) return BaseErrType.DataExist;

            var data = await _repository.FindAsync(entity.Id);
            if (data == null) return BaseErrType.DataNotFound;

            data.VisiableFields = entity.VisiableFields.ToJson();
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
            return await ResultAsync(() => _repository.DeleteAsync(data));
        }
    }
}
