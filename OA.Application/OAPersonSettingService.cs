using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OneForAll.Core;

namespace OA.Application
{
    /// <summary>
    /// 人员档案设置
    /// </summary>
    public class OAPersonSettingService : IOAPersonSettingService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonSettingManager _manager;
        private readonly IOAPersonSettingFieldManager _fieldManager;

        public OAPersonSettingService(
            IMapper mapper,
            IOAPersonSettingManager manager,
            IOAPersonSettingFieldManager fieldManager)
        {
            _mapper = mapper;
            _manager = manager;
            _fieldManager = fieldManager;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>组织架构树</returns>
        public async Task<IEnumerable<OAPersonSettingDto>> GetListAsync()
        {
            var data = await _manager.GetListAsync();
            return _mapper.Map<IEnumerable<OAPersonSettingAggr>, IEnumerable<OAPersonSettingDto>>(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAPersonSettingForm entity)
        {
            return await _manager.AddAsync(entity);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAPersonSettingForm entity)
        {
            return await _manager.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>列表</returns>
        public async Task<BaseErrType> DeleteAsync(Guid id)
        {
            return await _manager.DeleteAsync(id);
        }

        /// <summary>
        /// 重新排序
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> SortAsync(IEnumerable<Guid> ids)
        {
            return await _manager.SortAsync(ids);
        }

        #region 字段信息

        /// <summary>
        /// 添加字段
        /// </summary>
        /// <param name="id">设置id</param>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddFieldAsync(Guid id, OAPersonSettingFieldForm entity)
        {
            return await _fieldManager.AddAsync(id, entity);
        }

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="id">设置id</param>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateFieldAsync(Guid id, OAPersonSettingFieldForm entity)
        {
            return await _fieldManager.UpdateAsync(id, entity);
        }

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="fieldId">字段id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteFieldAsync(Guid id, Guid fieldId)
        {
            return await _fieldManager.DeleteAsync(id, fieldId);
        }

        /// <summary>
        /// 启用字段
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="fieldId">字段id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> EnableFieldAsync(Guid id, Guid fieldId)
        {
            return await _fieldManager.EnableAsync(id, fieldId);
        }

        /// <summary>
        /// 重新排序字段
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="fieldIds">字段Id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> SortFieldAsync(Guid id, IEnumerable<Guid> fieldIds)
        {
            return await _fieldManager.SortAsync(id, fieldIds);
        }
        #endregion
    }
}

