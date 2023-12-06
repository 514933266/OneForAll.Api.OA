using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 业务数据修复
    /// </summary>
    public interface IOARepairManager
    {
        /// <summary>
        /// 修复人员基础信息
        /// </summary>
        /// <returns>列表</returns>
        Task<BaseErrType> RepairPersonInfomation(Guid tenantId);
    }
}
