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

namespace OA.Host.Controllers
{
    /// <summary>
    /// 人员团队信息
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OAPersonTeamInfosController : BaseController
    {
        private readonly IOAPersonTeamInfoService _service;
        public OAPersonTeamInfosController(IOAPersonTeamInfoService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取人员（基础信息）
        /// </summary>
        /// <param name="key">人员姓名</param>
        /// <returns>基础信息</returns>
        [HttpGet]
        public async Task<IEnumerable<OAPersonTeamInfoDto>> GetListAsync([FromQuery] string key)
        {
            return await _service.GetListAsync(key);
        }

        /// <summary>
        /// 获取人员（基础信息）
        /// </summary>
        /// <param name="id">人员id</param>
        /// <returns>基础信息</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<OAPersonTeamInfoDto> GetAsync(Guid id)
        {
            return await _service.GetAsync(id);
        }
    }
}
