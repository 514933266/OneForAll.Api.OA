using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using OA.Public.Models;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.Enums;
using OA.Domain.Models;
using OneForAll.Core;
using OA.Host.Filters;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 自定义表格字段显示配置
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.PUBLIC)]
    public class OATableColumnSettingsController : BaseController
    {
        private readonly IOATableColumnSettingsService _service;
        public OATableColumnSettingsController(IOATableColumnSettingsService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取指定配置
        /// </summary>
        /// <param name="target">类型</param>
        /// <returns>指定模块配置</returns>
        [HttpGet]
        public async Task<OATableColumnSettingDto> GetAsync([FromQuery] OATableColumnSettingsTargetEnum target)
        {
            return await _service.GetAsync(target);
        }

        /// <summary>
        /// 创建或更新配置
        /// </summary>
        /// <param name="entity">组织id</param>
        /// <returns>结果</returns>
        [HttpPost]
        [CheckPermission(Controller = "OAPerson", Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> UpdateAsync([FromBody] OATableColumnSettingForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("保存成功");
                default: return msg.Fail("保存失败");
            }
        }
    }
}
