using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OneForAll.Core;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 钉钉接入设置
    /// </summary>
    public class OADingDingSettingService : IOADingDingSettingService
    {
        private readonly IMapper _mapper;
        private readonly IOADingDingSettingManager _manager;
        public OADingDingSettingService(
            IMapper mapper,
            IOADingDingSettingManager manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns>列表</returns>
        public async Task<OADingDingSettingDto> GetAsync()
        {
            var data = await _manager.GetAsync();
            return _mapper.Map<OADingDingSetting, OADingDingSettingDto>(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OADingDingSettingForm form)
        {
            if (form.Id == Guid.Empty)
            {
                return await _manager.AddAsync(form);
            }
            else
            {
                return await _manager.UpdateAsync(form);
            }
        }
    }
}
