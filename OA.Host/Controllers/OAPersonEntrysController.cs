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
using OA.Domain.Repositorys;
using OA.Repository;
using Microsoft.EntityFrameworkCore;
using IdentityModel;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 员工入职
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OAPersonEntrysController : BaseController
    {
        private readonly IOAPersonEntryService _service;
        public OAPersonEntrysController(IOAPersonEntryService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="creatorName">创建人</param>
        /// <param name="mobilePhone">手机号码</param>
        /// <param name="startDate">开始入职日期</param>
        /// <param name="endDate">开始入职日期</param>
        /// <returns>列表</returns>
        [HttpGet]
        public async Task<IEnumerable<OAPersonEntryDto>> GetListAsync(
             [FromQuery] string name = "",
             [FromQuery] string creatorName = "",
             [FromQuery] string mobilePhone = "",
             [FromQuery] DateTime? startDate = null,
             [FromQuery] DateTime? endDate = null)
        {
            return await _service.GetListAsync(name, creatorName, mobilePhone, startDate, endDate);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> AddAsync([FromBody] OAPersonEntryForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("人员/手机号码已有入职登记记录");
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
        public async Task<BaseMessage> UpdateAsync([FromBody] OAPersonEntryForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("人员/手机号码已有入职登记记录");
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
        /// 确认到岗
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="form">人员信息表单</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("{id}/IsConfirm")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> ConfirmAsync(Guid id, [FromBody] OAPersonEntryConfirmForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.ConfirmAsync(id, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("确认到岗成功");
                case BaseErrType.DataExist: return msg.Fail("员工信息已存在");
                case BaseErrType.NotAllow: return msg.Fail("员工未填写入职登记表");
                default: return msg.Fail("确认到岗失败");
            }
        }
    }
}
