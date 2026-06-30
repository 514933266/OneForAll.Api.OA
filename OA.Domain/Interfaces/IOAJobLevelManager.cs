using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OA.Domain.Models;
using OneForAll.Core;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 职级管理
    /// </summary>
    public interface IOAJobLevelManager
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<PageList<OAJobLevel>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAJobLevel>> GetListAsync(string key);

        /// <summary>
        /// 创建系统默认职级
        /// </summary>
        /// <returns></returns>
        Task<BaseErrType> CreateDefaultAsync();

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
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);
    }
}
