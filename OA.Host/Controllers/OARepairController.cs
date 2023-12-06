using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using OA.Host.Filters;
using OA.Domain.Models;
using OA.Public.Models;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.Interfaces;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 业务数据修复
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.RULER)]
    public class OARepairController : BaseController
    {
        private readonly IOARepairManager _manager;
        public OARepairController(IOARepairManager manager)
        {
            _manager = manager;
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
            msg.ErrType = await _manager.RepairPersonInfomation(tenantId);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修复成功");
                default: return msg.Fail("修复失败");
            }
        }
    }
}
