using OA.Application.Dtos;
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
    /// 人员档案设置
    /// </summary>
    public interface IOAPersonSettingService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPersonSettingDto>> GetListAsync();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OAPersonSettingForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OAPersonSettingForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid id);

        /// <summary>
        /// 重新排序
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> SortAsync(IEnumerable<Guid> ids);

        #region 字段信息

        /// <summary>
        /// 添加字段
        /// </summary>
        /// <param name="id">设置id</param>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddFieldAsync(Guid id, OAPersonSettingFieldForm entity);

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="id">设置id</param>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateFieldAsync(Guid id, OAPersonSettingFieldForm entity);

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="fieldId">字段id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteFieldAsync(Guid id, Guid fieldId);

        /// <summary>
        /// 启用字段
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="fieldId">字段id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> EnableFieldAsync(Guid id, Guid fieldId);

        /// <summary>
        /// 重新排序
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="fieldIds">字段Id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> SortFieldAsync(Guid id, IEnumerable<Guid> fieldIds);

        #endregion
    }
}
