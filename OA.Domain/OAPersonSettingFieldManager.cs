using AutoMapper;
using Microsoft.AspNetCore.Http;
using OA.Domain.AggregateRoots;
using OA.Domain.Enums;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OA.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain
{
    /// <summary>
    /// 人员档案字段设置
    /// </summary>
    public class OAPersonSettingFieldManager : OABaseManager, IOAPersonSettingFieldManager
    {
        private readonly IOAPersonSettingFieldRepository _repository;
        private readonly IOAPersonSettingRepository _settingRepository;

        public OAPersonSettingFieldManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOAPersonSettingFieldRepository repository,
            IOAPersonSettingRepository settingRepository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
            _settingRepository = settingRepository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid settingId, OAPersonSettingFieldForm entity)
        {
            var exists = await _repository.GetAsync(w => w.Text == entity.Text);
            if (exists != null) return BaseErrType.DataExist;
            var setting = await _settingRepository.FindAsync(settingId);
            if (setting == null) return BaseErrType.DataError;

            var data = _mapper.Map<OAPersonSettingField>(entity);
            data.Name = data.Text;
            data.IsEnabled = true;
            data.IsShowEnabled = true;
            data.IsEnableText = true;
            data.IsEnableType = true;
            data.IsEnableRequired = true;
            data.OAPersonSettingId = settingId;
            data.IsEnableEmployeeEditable = true;
            data.IsEnableEmployeeVisiable = true;
            data.TypeDetail = data.Type == OAPersonSettingFieldTypeEnum.Select ? data.TypeDetail : "";
            data.IsEnableAddTypeDetail = data.Type == OAPersonSettingFieldTypeEnum.Select ? true : false;

            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid settingId, OAPersonSettingFieldForm entity)
        {
            var exists = await _repository.FindAsync(entity.Id);
            if (exists != null && exists.Id != entity.Id) return BaseErrType.DataExist;

            var data = await _repository.FindAsync(entity.Id);
            if (data == null) return BaseErrType.DataNotFound;
            if (data.OAPersonSettingId != settingId) return BaseErrType.DataError;

            data.Update(entity);
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid settingId, Guid id)
        {
            var totalData = await _repository.GetListBySettingAsync(settingId);
            var data = totalData.FirstOrDefault(w => w.Id == id);
            if (data == null) return BaseErrType.DataNotFound;
            if (data.IsDefault) return BaseErrType.NotAllow;

            // 重新排序
            var index = 0;
            var effected = 0;
            var updateData = totalData.Where(w => w.Id != id).ToList();
            updateData.ForEach(e =>
            {
                e.SortNumber = index;
                index++;
            });

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                updateData.ForEach(e =>
                {
                    _repository.Update(e, tran);
                });
                _repository.Delete(data, tran);
                effected = tran.Commit();
            }
            return Result(() => effected);
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> EnableAsync(Guid settingId, Guid id)
        {
            var data = await _repository.FindAsync(id);
            if (data == null) return BaseErrType.DataNotFound;
            if (data.OAPersonSettingId != settingId) return BaseErrType.DataError;
            if (!data.IsShowEnabled) return BaseErrType.NotAllow;

            data.IsEnabled = !data.IsEnabled;
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 重新排序
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> SortAsync(Guid settingId, IEnumerable<Guid> ids)
        {
            var data = await _repository.GetListAsync(ids);
            if (!data.Any()) return BaseErrType.DataNotFound;
            if (data.Any(w => w.OAPersonSettingId != settingId)) return BaseErrType.DataError;

            // 重新排序
            var index = 0;
            ids.ForEach(e =>
            {
                var item = data.FirstOrDefault(w => w.Id == e);
                if (item != null)
                {
                    item.SortNumber = index;
                    index++;
                }
            });
            return await ResultAsync(() => _repository.UpdateRangeAsync(data));
        }
    }
}
