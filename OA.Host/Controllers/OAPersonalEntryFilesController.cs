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
using OneForAll.Core.Upload;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 员工入职资料填写
    /// </summary>
    [Route("api/[controller]")]
    public class OAPersonalEntryFilesController : BaseController
    {
        private readonly IOAPersonalEntryFileService _service;
        public OAPersonalEntryFilesController(IOAPersonalEntryFileService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取入职登记
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>列表</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<OAPersonalEntryFileDto> GetAsync(Guid id)
        {
            return await _service.GetAsync(id);
        }

        /// <summary>
        /// 获取类型列表
        /// </summary>
        /// <param name="id">入职申请的id</param>
        /// <returns>组织架构树</returns>
        [HttpGet]
        [Route("{id}/Settings")]
        public async Task<IEnumerable<OAPersonSettingDto>> GetListSettingAsync(Guid id)
        {
            return await _service.GetListSettingAsync(id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPut]
        public async Task<BaseMessage> UpdateAsync([FromBody] OAPersonalEntryFileForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("资料更新成功");
                default: return msg.Fail("资料更新失败");
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="id">人员id</param>
        /// <param name="form">文件流</param>
        /// <returns>结果（头像的路径会在Data中输出）</returns>
        [HttpPost]
        [Route("{id}/Files")]
        public async Task<BaseMessage> UploadFileAsync(Guid id, [FromForm] IFormCollection form)
        {
            var msg = new BaseMessage();
            if (form.Files.Count > 0)
            {
                var file = form.Files.FirstOrDefault();

                var callbacks = await _service.UploadFileAsync(id, file.FileName, file.OpenReadStream());

                msg.Data = new { Username = LoginUser.UserName, Result = callbacks };

                switch (callbacks.State)
                {
                    case UploadEnum.Success: return msg.Success("上传成功");
                    case UploadEnum.Overflow: return msg.Fail("上传图片最大不能超过5MB!");
                    case UploadEnum.TypeError: return msg.Fail("上传图片只支持jpg、jpeg、png格式!");
                    case UploadEnum.Error: return msg.Fail("上传过程中发生未知错误");
                }
            }
            return msg.Fail("上传失败，请选择文件");
        }
    }
}
