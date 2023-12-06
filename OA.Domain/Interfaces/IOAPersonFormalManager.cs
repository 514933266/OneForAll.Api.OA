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
    /// 转正管理
    /// </summary>
    public interface IOAPersonFormalManager
    {
        /// <summary>
        /// 获取待转正列表
        /// </summary>
        /// <param name="name">员工姓名</param>
        /// <param name="teamId">团队id</param>
        /// <param name="planStartDate">计划转正-开始日期</param>
        /// <param name="planEndDate">计划转正-开始日期</param>
        /// <param name="actualStartDate">实际转正-开始日期</param>
        /// <param name="actualEndDate">实际转正-开始日期</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPersonFormalAggr>> GetListAsync(
            string name,
            Guid teamId,
            DateTime? planStartDate,
            DateTime? planEndDate,
            DateTime? actualStartDate,
            DateTime? actualEndDate);

        /// <summary>
        /// 办理转正
        /// </summary>
        /// <param name="id">数据id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> ConfirmAsync(Guid id, OAPersonFormalConfirmForm form);
    }
}
