using System;
using System.Threading.Tasks;
using OneForAll.Core;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 业务数据修复
    /// </summary>
    public interface IOARepairService
    {
        /// <summary>
        /// 修复人员基础信息
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> RepairPersonInfomation(Guid tenantId);
    }
}
