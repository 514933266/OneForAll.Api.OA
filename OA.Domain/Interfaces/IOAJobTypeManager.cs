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
    /// 职级类型
    /// </summary>
    public interface IOAJobTypeManager
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAJobType>> GetListAsync(string key);

        /// <summary>
        /// 创建系统默认职位分类
        /// </summary>
        /// <returns></returns>
        Task<BaseErrType> CreateDefaultAsync();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OAJobTypeForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OAJobTypeForm form);

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);
    }
}
