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
    /// 个人日程
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OAPersonalSchedulesController : BaseController
    {
        private readonly IOAPersonalScheduleService _service;
        public OAPersonalSchedulesController(IOAPersonalScheduleService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="date">月份</param>
        /// <param name="isClosed">是否已关闭(不提供该参数时返回全部)</param>
        /// <returns>人员分页</returns>
        [HttpGet]
        [Route("{date}")]
        public async Task<IEnumerable<OAPersonalScheduleDto>> GetListAsync(DateTime date, [FromQuery] bool? isClosed)
        {
            return await _service.GetListAsync(date, isClosed);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromBody] OAPersonalScheduleForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("请勿重复添加日程");
                case BaseErrType.DataNotMatch: return msg.Fail("日程时间小于当前日期");
                case BaseErrType.NotAllow: return msg.Fail("日程已过期或关闭");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">日程id</param>
        /// <returns>消息</returns>
        [HttpPatch]
        [Route("Batch/IsDeleted")]
        public async Task<BaseMessage> DeleteAsync([FromBody] IEnumerable<Guid> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataEmpty: return msg.Success("日程不存在");
                default: return msg.Fail("删除失败");
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="id">日程id</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("{id}/IsClosed")]
        public async Task<BaseMessage> CloseAsync(Guid id)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.CloseAsync(id);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("关闭成功");
                case BaseErrType.DataEmpty: return msg.Success("日程不存在");
                default: return msg.Fail("关闭失败");
            }
        }
    }
}
