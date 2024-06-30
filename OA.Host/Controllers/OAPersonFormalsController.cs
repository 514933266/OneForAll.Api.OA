using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Public.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.SqlServer.Types;
using OA.Domain.Models;
using OA.Host.Filters;
using OneForAll.Core;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 转正管理
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OAPersonFormalsController : BaseController
    {
        private readonly IOAPersonFormalService _service;
        public OAPersonFormalsController(IOAPersonFormalService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="name">员工姓名</param>
        /// <param name="teamId">团队id</param>
        /// <param name="planStartDate">计划转正-开始日期</param>
        /// <param name="planEndDate">计划转正-开始日期</param>
        /// <param name="actualStartDate">实际转正-开始日期</param>
        /// <param name="actualEndDate">实际转正-开始日期</param>
        /// <returns>列表</returns>
        [HttpGet]
        public async Task<IEnumerable<OAPersonFormalDto>> GetListAsync(
            [FromQuery] string name,
            [FromQuery] Guid? teamId,
            [FromQuery] DateTime? planStartDate,
            [FromQuery] DateTime? planEndDate,
            [FromQuery] DateTime? actualStartDate,
            [FromQuery] DateTime? actualEndDate)
        {
            if (teamId == null)
                teamId = Guid.Empty;
            return await _service.GetListAsync(name, teamId.Value, planStartDate, planEndDate, actualStartDate, actualEndDate);
        }

        /// <summary>
        /// 办理转正
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("{id}/IsConfirm")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> ConfirmAsync(Guid id, [FromBody] OAPersonFormalConfirmForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.ConfirmAsync(id, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("办理转正成功");
                case BaseErrType.DataExist: return msg.Fail("员工信息已存在");
                case BaseErrType.DataNotFound: return msg.Fail("员工信息不存在");
                default: return msg.Fail("办理转正失败");
            }
        }
    }
}
