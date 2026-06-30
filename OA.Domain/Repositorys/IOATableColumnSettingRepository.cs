using OA.Domain.AggregateRoots;
using OA.Domain.Enums;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Repositorys
{
    /// <summary>
    /// 表格自定义字段显示配置
    /// </summary>
    public interface IOATableColumnSettingsRepository : IEFCoreRepository<OATableColumnSetting>
    {
        /// <summary>
        /// 查询指定用户配置
        /// </summary>
        /// <param name="target">类型</param>
        /// <param name="creatorId">创建人id</param>
        /// <returns></returns>
        Task<OATableColumnSetting> GetAsync(OATableColumnSettingsTargetEnum target, Guid creatorId);
    }
}
