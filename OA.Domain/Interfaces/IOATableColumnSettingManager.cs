using OA.Domain.AggregateRoots;
using OA.Domain.Enums;
using OA.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 表格自定义字段显示配置
    /// </summary>
    public interface IOATableColumnSettingManager
    {
        /// <summary>
        /// 获取个人配置
        /// </summary>
        /// <param name="target">目标类型</param>
        /// <returns>列表</returns>
        Task<OATableColumnSetting> GetAsync(OATableColumnSettingsTargetEnum target);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OATableColumnSettingForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OATableColumnSettingForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">团队组织id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid id);
    }
}
