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
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<PageList<OAPersonEntry>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="creatorName">创建人</param>
        /// <param name="mobilePhone">手机号码</param>
        /// <param name="startDate">开始入职日期</param>
        /// <param name="endDate">开始入职日期</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPersonEntry>> GetListAsync(
             string name,
             string creatorName,
             string mobilePhone,
             DateTime? startDate,
             DateTime? endDate);

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
    }
}
