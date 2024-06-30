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
using OneForAll.Core.Utility;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 员工关怀
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OAPersonCaresController : BaseController
    {
        private readonly IOAPersonBirthdayCareService _service;
        public OAPersonCaresController(IOAPersonBirthdayCareService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取员工生日列表
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>列表</returns>
        [HttpGet]
        [Route("Birthday")]
        public async Task<IEnumerable<OAPersonBirthdayCareDto>> GetListBirthdayAsync([FromQuery] Guid teamId = default, [FromQuery] DateTime? startDate = default, [FromQuery] DateTime? endDate = default)
        {
            if (startDate == null && endDate == null)
            {
                startDate = TimeHelper.ConvertToFirstDate(DateTime.Now);
                endDate = TimeHelper.ConvertToLastDate(DateTime.Now);
            }
            return await _service.GetListBirthdayAsync(teamId, startDate.Value, endDate.Value);
        }

        /// <summary>
        /// 获取入职周年列表
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="date">开始日期</param>
        /// <returns>列表</returns>
        [HttpGet]
        [Route("Company")]
        public async Task<IEnumerable<OAPersonCompanyCareDto>> GetListCompanyAsync([FromQuery] Guid teamId = default, [FromQuery] DateTime? date = default)
        {
            if (date == null)
            {
                date = TimeHelper.ConvertToFirstDate(DateTime.Now);
            }
            return await _service.GetListCompanyAsync(teamId, date.Value);
        }
    }
}
