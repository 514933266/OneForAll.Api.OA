using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OneForAll.Core.Upload;
using OA.Domain.Enums;
using OA.Domain.Models;
using OA.Application.Dtos;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 职位管理
    /// </summary>
    public interface IOAJobService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="isEnabled">是否启用</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<IEnumerable<OAJobDto>> GetListAsync(bool? isEnabled, string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OAJobForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OAJobForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 启用/停用
        /// </summary>
        /// <param name="id">菜单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> SetIsEnabledAsync(Guid id);
    }
}
