using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using OneForAll.Core.Upload;
using OA.Host.Filters;
using OA.Domain.Enums;
using OA.Domain.Models;
using OA.Public.Models;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OneForAll.Core.Extension;
using OneForAll.Core.DDD;
using OA.Application;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 人事历程
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OAPersonalTeamHistoriesController : BaseController
    {
        private readonly IOAPersonalTeamHistoryService _service;
        public OAPersonalTeamHistoriesController(IOAPersonalTeamHistoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取人事历程列表
        /// </summary>
        /// <returns>列表</returns>
        [HttpGet]
        public async Task<IEnumerable<OATeamMemberHistoryDto>> GetListAsync()
        {
            return await _service.GetListAsync(LoginUser);
        }
    }
}
