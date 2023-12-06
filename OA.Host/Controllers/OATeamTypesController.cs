using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using OA.Public.Models;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Host.Filters;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 部门类型
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.PUBLIC)]
    public class OATeamTypesController : BaseController
    {
        private readonly IOATeamTypeService _service;
        public OATeamTypesController(IOATeamTypeService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取类型列表
        /// </summary>
        /// <param name="name">类型</param>
        /// <returns>组织架构树</returns>
        [HttpGet]
        public async Task<IEnumerable<OATeamTypeDto>> GetListAsync([FromQuery] string name = default)
        {
            return await _service.GetListAsync(name);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">组织id</param>
        /// <returns>结果</returns>
        [HttpDelete("{id}")]
        [CheckPermission(Controller = "OATeam", Action = ConstPermission.EnterView)]
        public async Task DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);
        }
    }
}
