using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using OA.Domain.Enums;
using OA.Domain.Models;
using OA.Public.Models;
using OA.Host.Filters;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 离职登记
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.PUBLIC)]
    public class OAPersonLeavesController : BaseController
    {
        private readonly IOAPersonLeaveService _service;
        public OAPersonLeavesController(IOAPersonLeaveService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="creatorName">创建人</param>
        /// <param name="teamId">部门</param>
        /// <param name="startDate">开始入职日期</param>
        /// <param name="endDate">开始入职日期</param>
        /// <returns>列表</returns>
        [HttpGet]
        public async Task<IEnumerable<OAPersonLeaveDto>> GetListAsync(
             [FromQuery] string name = "",
             [FromQuery] string creatorName = "",
             [FromQuery] Guid? teamId = null,
             [FromQuery] DateTime? startDate = null,
             [FromQuery] DateTime? endDate = null)
        {
            return await _service.GetListAsync(name, creatorName, teamId, startDate, endDate);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> AddAsync([FromBody] OAPersonLeaveForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("数据已存在");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPut]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> UpdateAsync([FromBody] OAPersonLeaveForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("数据已存在");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>消息</returns>
        [HttpPatch]
        [Route("Batch/IsDeleted")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> DeleteAsync([FromBody] IEnumerable<Guid> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataEmpty: return msg.Success("数据不存在");
                default: return msg.Fail("删除失败");
            }
        }

        /// <summary>
        /// 确认离职
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="form">人员信息表单</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("{id}/IsConfirm")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> ConfirmAsync(Guid id, [FromBody] OAPersonLeaveConfirmForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.ConfirmAsync(id, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("确认离职成功");
                default: return msg.Fail("确认离职失败");
            }
        }
    }
}
