using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using OneForAll.Core.OAuth;
using OA.Application.Interfaces;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 业务数据修复
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.Ruler)]
    public class OARepairController : BaseController
    {
        private readonly IOARepairService _service;
        public OARepairController(IOARepairService service)
        {
            _service = service;
        }

        /// <summary>
        /// 修复人员基础信息
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>列表</returns>
        [HttpGet]
        [Route("PersonInfomation")]
        public async Task<BaseMessage> RepairPersonInfomation([FromQuery] Guid tenantId)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.RepairPersonInfomation(tenantId);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修复成功");
                default: return msg.Fail("修复失败");
            }
        }
    }
}
