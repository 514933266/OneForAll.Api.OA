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
using OA.Domain.Models;
using OneForAll.Core;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 部门类型
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
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
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> AddAsync([FromBody] OATeamTypeForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("类型已存在");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">组织id</param>
        /// <returns>结果</returns>
        [HttpDelete("{id}")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> DeleteAsync(Guid id)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(id);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataNotFound: return msg.Fail("数据不存在");
                default: return msg.Fail("删除失败");
            }
        }
    }
}
