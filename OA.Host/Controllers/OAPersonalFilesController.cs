using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OA.Application.Interfaces;
using OA.Public.Models;
using OneForAll.Core;
using System.Threading.Tasks;
using System;
using OA.Application.Dtos;
using OA.Domain.Models;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 用户档案
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OAPersonalFilesController : BaseController
    {
        private readonly IOAPersonalFileService _service;
        public OAPersonalFilesController(IOAPersonalFileService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取个人档案
        /// </summary>
        /// <returns>结果</returns>
        public async Task<OAPersonDto> GetAsync()
        {
            return await _service.GetAsync();
        }

        /// <summary>
        /// 查找并绑定个人档案
        /// </summary>
        /// <param name="form">手机|身份证</param>
        /// <returns>结果</returns>
        [HttpPost]
        public async Task<BaseMessage> BindAsync([FromBody] OABindPersonalFileForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.BindAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("绑定档案成功");
                case BaseErrType.DataNotFound: return msg.Fail("未找到员工档案");
                case BaseErrType.NotAllow: return msg.Fail("档案已关联到其他账号，请联系企业管理员修改");
                default: return msg.Fail("绑定档案失败");
            }
        }
    }
}
