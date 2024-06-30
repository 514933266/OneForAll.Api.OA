using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Public.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.SqlServer.Types;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 合同管理
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OAPersonContractsController : BaseController
    {
        private readonly IOAPersonContractService _service;
        public OAPersonContractsController(IOAPersonContractService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <returns>列表</returns>
        [HttpGet]
        public async Task<IEnumerable<OAPersonContractDto>> GetListAsync([FromQuery] Guid? teamId)
        {
            if (teamId == null)
                teamId = Guid.Empty;
            return await _service.GetListAsync(teamId.Value);
        }
    }
}
