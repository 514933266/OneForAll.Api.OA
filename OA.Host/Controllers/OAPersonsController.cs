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
    /// 人员档案
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OAPersonsController : BaseController
    {
        private readonly IOAPersonService _service;
        public OAPersonsController(IOAPersonService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>人员分页</returns>
        [HttpGet]
        [Route("{pageIndex}/{pageSize}")]
        public async Task<PageList<OAPersonDto>> GetPageAsync(
            int pageIndex,
            int pageSize,
            [FromQuery] string key = default,
            [FromQuery] OAPersonOnJobStatusEnum onJobStatus = OAPersonOnJobStatusEnum.None)
        {
            return await _service.GetPageAsync(pageIndex, pageSize, key, onJobStatus);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>列表</returns>
        [HttpGet]
        public async Task<IEnumerable<OAPersonDto>> GetListAsync([FromQuery] string key = default, [FromQuery] OAPersonOnJobStatusEnum onJobStatus = OAPersonOnJobStatusEnum.None)
        {
            if (key.IsNullOrEmpty()) return new List<OAPersonDto>();

            return await _service.GetListAsync(key, onJobStatus);
        }

        /// <summary>
        /// 获取人员（基础信息）
        /// </summary>
        /// <param name="id">人员id</param>
        /// <returns>基础信息</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<OAPersonDto> GetAsync(Guid id)
        {
            return await _service.GetAsync(id);
        }

        

        /// <summary>
        /// 获取统计信息
        /// </summary>
        /// <returns>列表</returns>
        [HttpGet]
        [Route("Statistic")]
        public async Task<OAPersonStatisticDto> GetStatisticsAsync()
        {
            return await _service.GetStatisticsAsync();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        [HttpPost]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> AddAsync([FromBody] OAPersonForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("员工信息已存在");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        [HttpPut]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> UpdateAsync([FromBody] OAPersonForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("员工信息已存在");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 头像上传
        /// </summary>
        /// <param name="id">人员id</param>
        /// <param name="url">头像地址</param>
        /// <returns>结果</returns>
        [HttpPost]
        [Route("{id}/Header")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> UpdateHeaderAsync(Guid id, [FromBody] string url)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateHeaderAsync(id, url);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("头像设置成功");
                case BaseErrType.DataNotFound: return msg.Fail("员工信息不存在");
                default: return msg.Fail("头像设置失败");
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
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> UploadFileAsync(Guid id, [FromForm] IFormCollection form)
        {
            var msg = new BaseMessage();
            if (form.Files.Count > 0)
            {
                // 第一次添加数据时上传图片
                if (id == Guid.Empty)
                    id = Guid.NewGuid();
                var file = form.Files.FirstOrDefault();
                var callbacks = await _service.UploadFileAsync(id, file.FileName, file.OpenReadStream());
                msg.Data = new { Username = LoginUser.UserName, Id = id, Result = callbacks };
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

        /// <summary>
        /// 离职（批量）
        /// </summary>
        /// <param name="ids">人员id</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("Batch/LeaveDate")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> LeaveAsync([FromBody] IEnumerable<Guid> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.LeaveAsync(ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("离职成功");
                default: return msg.Fail("离职失败");
            }
        }

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">人员id</param>
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
        /// 导出Excel
        /// </summary>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <param name="employeeStatus">员工状态</param>
        /// <param name="employeeType">员工类型</param>
        /// <param name="fields">选择导出字段</param>
        /// <param name="startEntryDate">开始入职时间</param>
        /// <param name="endEntryDate">结束入职时间</param>
        /// <returns>文件流</returns>
        [HttpGet]
        [Route("Excel")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<IActionResult> ExportExcelAsync(
            [FromQuery] OAPersonOnJobStatusEnum onJobStatus,
            [FromQuery] string employeeType,
            [FromQuery] string employeeStatus,
            [FromQuery] IEnumerable<string> fields,
            [FromQuery] DateTime? startEntryDate,
            [FromQuery] DateTime? endEntryDate)
        {
            var file = await _service.ExportExcelAsync(onJobStatus, employeeType, employeeStatus, fields, startEntryDate, endEntryDate);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "人员资料.xlsx");
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="form">文件表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [Route("Excel")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> ImportExcelAsync([FromForm] IFormCollection form)
        {
            var msg = new BaseMessage();
            var file = form.Files.First();

            msg.ErrType = await _service.ImportExcelAsync(file.FileName, file.OpenReadStream());
            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("导入成功");
                case BaseErrType.DataEmpty: return msg.Fail("请选择文件");
                case BaseErrType.DataError: return msg.Fail("数据异常");
                case BaseErrType.NotAllow: return msg.Fail("请选择Excel文件导入");
                case BaseErrType.DataNotFound: return msg.Fail("没有可以导入的数据");
                default: return msg.Fail("导入失败");
            }
        }
    }
}
