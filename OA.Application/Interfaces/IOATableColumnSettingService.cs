using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OA.Application.Dtos;
using OA.Domain.Enums;
using OA.Domain.Models;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 表格自定义字段显示配置
    /// </summary>
    public interface IOATableColumnSettingsService
    {
        /// <summary>
        /// 获取个人配置
        /// </summary>
        /// <param name="target">目标类型</param>
        /// <returns>列表</returns>
        Task<OATableColumnSettingDto> GetAsync(OATableColumnSettingsTargetEnum target);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OATableColumnSettingForm entity);
    }
}
