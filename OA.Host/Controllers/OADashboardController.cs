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

namespace OA.Host.Controllers
{
    /// <summary>
    /// 人员档案
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.PUBLIC)]
    public class OADashboardController : BaseController
    {
        private readonly IOADashboardService _service;
        public OADashboardController(IOADashboardService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取新入职员工列表（近2个月）
        /// </summary>
        /// <returns>列表</returns>
        [HttpGet]
        [Route("NewPersons")]
        public async Task<IEnumerable<OAPersonBasicInfoDto>> GetListAsync()
        {
            return await _service.GetListNewPersonAsync();
        }

        /// <summary>
        /// 获取人员数量统计
        /// </summary>
        /// <returns>列表</returns>
        [HttpGet]
        [Route("Statistics")]
        public async Task<OAPersonStatisticV2Dto> GetStatisticsAsync()
        {
            return await _service.GetStatisticsAsync();
        }
    }
}
