using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.Enums;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 自定义表格字段显示配置
    /// </summary>
    public class OATableColumnSettingsService : IOATableColumnSettingsService
    {
        private readonly IMapper _mapper;
        private readonly IOATableColumnSettingManager _manager;

        public OATableColumnSettingsService(
            IMapper mapper,
            IOATableColumnSettingManager manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        /// <summary>
        /// 获取个人配置
        /// </summary>
        /// <param name="target">目标类型</param>
        /// <returns>列表</returns>
        public async Task<OATableColumnSettingDto> GetAsync(OATableColumnSettingsTargetEnum target)
        {
            var data = await _manager.GetAsync(target);
            return _mapper.Map<OATableColumnSettingDto>(data);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OATableColumnSettingForm entity)
        {
            if (entity.Id == Guid.Empty)
            {
                return await _manager.AddAsync(entity);
            }
            else
            {
                return await _manager.UpdateAsync(entity);
            }
        }
    }
}
