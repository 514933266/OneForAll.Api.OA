using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
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
    /// 人员档案设置
    /// </summary>
    public interface IOAPersonSettingManager
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPersonSettingAggr>> GetListAsync();

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
    }
}
