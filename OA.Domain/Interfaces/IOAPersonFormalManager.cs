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
        /// 办理转正
        /// </summary>
        /// <param name="id">数据id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> ConfirmAsync(Guid id, OAPersonFormalConfirmForm form);
    }
}
