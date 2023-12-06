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
    /// 人员档案字段设置
    /// </summary>
    public interface IOAPersonSettingFieldManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid settingId, OAPersonSettingFieldForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid settingId, OAPersonSettingFieldForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid settingId, Guid id);

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> EnableAsync(Guid settingId, Guid id);

        /// <summary>
        /// 重新排序
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> SortAsync(Guid settingId, IEnumerable<Guid> ids);
    }
}
