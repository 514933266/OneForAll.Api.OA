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
    /// 团队成员异动历史
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.PUBLIC)]
    public class OATeamMemberHistorysController : BaseController
    {
        private readonly IOATeamMemberHistoryService _service;
        public OATeamMemberHistorysController(IOATeamMemberHistoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取类型列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="teamId"></param>
        /// <param name="key"></param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>列表</returns>
        [HttpGet]
        [Route("{pageIndex}/{pageSize}")]
        public async Task<PageList<OATeamMemberHistoryDto>> GetPageAsync(
            int pageIndex,
            int pageSize,
            [FromQuery] Guid teamId = default,
            [FromQuery] string key = default,
            [FromQuery] DateTime? startDate = default,
            [FromQuery] DateTime? endDate = default)
        {
            return await _service.GetPageAsync(pageIndex, pageSize, teamId, key, startDate, endDate);
        }
    }
}
