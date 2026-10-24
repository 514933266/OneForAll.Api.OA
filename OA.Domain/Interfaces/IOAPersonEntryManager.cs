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
    /// 员工入职
    /// </summary>
    public interface IOAPersonEntryManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OAPersonEntryForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OAPersonEntryForm form);

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 确认到岗
        /// </summary>
        /// <param name="id">数据id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> ConfirmAsync(Guid id);

        /// <summary>
        /// 填写资料
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdatePersonFileAsync(OAPersonalEntryFileForm form);

        /// <summary>
        /// 由入职登记与到岗表单构建人员档案表单（含附加信息默认值规则）
        /// </summary>
        /// <param name="entry">入职登记</param>
        /// <param name="form">到岗表单</param>
        /// <returns>人员档案表单</returns>
        Task<OAPersonForm> BuildPersonFormAsync(OAPersonEntryAggr entry, OAPersonEntryConfirmForm form);
    }
}
