using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OA.Domain.Models;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 部门类型
    /// </summary>
    public interface IOATeamTypeManager
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamType>> GetListAsync(string name);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OATeamTypeForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OATeamTypeForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">团队组织id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid id);
    }
}
