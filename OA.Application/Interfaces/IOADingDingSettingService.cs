using OA.Application.Dtos;
using OA.Domain.AggregateRoots;
using OA.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 钉钉接入设置
    /// </summary>
    public interface IOADingDingSettingService
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns>列表</returns>
        Task<OADingDingSettingDto> GetAsync();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OADingDingSettingForm form);
    }
}
