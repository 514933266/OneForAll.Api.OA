using System;
using System.Threading.Tasks;
using OneForAll.Core;
using OA.Domain.Interfaces;
using OA.Application.Interfaces;

namespace OA.Application
{
    /// <summary>
    /// 业务数据修复
    /// </summary>
    public class OARepairService : IOARepairService
    {
        private readonly IOARepairManager _manager;

        public OARepairService(IOARepairManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 修复人员基础信息
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> RepairPersonInfomation(Guid tenantId)
        {
            return await _manager.RepairPersonInfomation(tenantId);
        }
    }
}
