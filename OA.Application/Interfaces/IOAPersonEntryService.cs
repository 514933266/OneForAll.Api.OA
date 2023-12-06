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
    /// 员工入职
    /// </summary>
    public interface IOAPersonEntryService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="creatorName">创建人</param>
        /// <param name="mobilePhone">手机号码</param>
        /// <param name="startDate">开始入职日期</param>
        /// <param name="endDate">开始入职日期</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPersonEntryDto>> GetListAsync(
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
        /// 删除
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 确认到岗
        /// </summary>
        /// <param name="id">数据id</param>
        /// <param name="form">人员信息表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> ConfirmAsync(Guid id, OAPersonEntryConfirmForm form);
    }
}
