using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Enums;
using OA.Domain.Models;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 企业组织架构
    /// </summary>
    public interface IOATeamManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OATeamForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OATeamForm entity);

        /// <summary>
        /// 移除/恢复
        /// </summary>
        /// <param name="id">团队组织id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid id);

        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="id">组织架构id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> TrueDeleteAsync(Guid id);

        /// <summary>
        /// 批量排序
        /// </summary>
        /// <param name="ids">排序表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> SortAsync(IEnumerable<Guid> ids);
    }
}
