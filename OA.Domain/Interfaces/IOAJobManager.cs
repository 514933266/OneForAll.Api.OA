using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OA.Domain.Models;
using OneForAll.Core;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 职位管理
    /// </summary>
    public interface IOAJobManager
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<PageList<OAJob>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="isEnabled">是否启用</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAJobAggr>> GetListAsync(bool? isEnabled, string key);

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
        /// 删除（批量）
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
