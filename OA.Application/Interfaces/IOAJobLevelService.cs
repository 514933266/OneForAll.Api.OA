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
    /// 职级管理
    /// </summary>
    public interface IOAJobLevelService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<IEnumerable<OAJobLevelDto>> GetListAsync(string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OAJobLevelForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OAJobLevelForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);
    }
}
