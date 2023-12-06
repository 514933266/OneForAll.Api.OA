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
    /// 职位管理
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OAJobsController : BaseController
    {
        private readonly IOAJobService _service;
        public OAJobsController(IOAJobService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="isEnabled">是否启用</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        [HttpGet]
        public async Task<IEnumerable<OAJobDto>> GetListAsync([FromQuery] bool? isEnabled, [FromQuery] string key = default)
        {
            return await _service.GetListAsync(isEnabled, key);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> AddAsync([FromBody] OAJobForm form)
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
        public async Task<BaseMessage> UpdateAsync([FromBody] OAJobForm form)
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
        /// 启用
        /// </summary>
        /// <param name="id">菜单表单</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("{id}/IsEnabled")]
        public async Task<BaseMessage> UpdateAsync(Guid id)
        {
            var msg = new BaseMessage() { Status = false };
            msg.ErrType = await _service.SetIsEnabledAsync(id);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("操作成功");
                case BaseErrType.DataNotFound: return msg.Fail("数据不存在");
                default: return msg.Fail("操作失败");
            }
        }
    }
}
