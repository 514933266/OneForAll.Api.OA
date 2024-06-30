using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using OA.Host.Filters;
using OA.Domain.Models;
using OA.Public.Models;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Application;
using OneForAll.Core.DDD;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 钉钉接入配置
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OADingDingSettingsController : BaseController
    {
        private readonly IOADingDingSettingService _service;
        public OADingDingSettingsController(IOADingDingSettingService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns>组织架构树</returns>
        [HttpGet]
        [Route("Top")]
        public async Task<OADingDingSettingDto> GetAsync()
        {
            return await _service.GetAsync();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromBody] OADingDingSettingForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success:
                    return msg.Success("保存成功");
                default: return msg.Fail("保存失败");
            }
        }
    }
}
