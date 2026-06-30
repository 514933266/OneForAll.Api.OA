using OA.Application.Dtos;
using OA.Domain.AggregateRoots;
using OA.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 个人日程
    /// </summary>
    public interface IOAPersonalScheduleService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="date">月份</param>
        /// <param name="isClosed">是否已关闭</param>
        /// <returns>分页列表</returns>
        Task<IEnumerable<OAPersonalScheduleDto>> GetListAsync(DateTime date, bool? isClosed);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OAPersonalScheduleForm form);

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">项目id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="id">日程id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> CloseAsync(Guid id);
    }
}
