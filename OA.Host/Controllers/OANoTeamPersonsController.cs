using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using OA.Host.Filters;
using OA.Domain.Models;
using OA.Public.Models;
using OA.Application.Dtos;
using OA.Application.Interfaces;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 未加入团队成员
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OANoTeamPersonsController
    {
        private readonly IOATeamMemberService _service;
        public OANoTeamPersonsController(IOATeamMemberService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>结果</returns>
        [HttpGet]
        public async Task<IEnumerable<OANoTeamPersonDto>> GetListAsync()
        {
            return await _service.GetListNoTeamAsync();
        }
    }
}
