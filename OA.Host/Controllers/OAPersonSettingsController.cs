using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using OA.Host.Filters;
using OA.Domain.Models;
using OA.Public.Models;
using OA.Application.Dtos;
using OA.Application.Interfaces;

namespace OA.Host.Controllers
{
    /// <summary>
    /// 人员档案设置
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OAPersonSettingsController : BaseController
    {
        private readonly IOAPersonSettingService _service;
        public OAPersonSettingsController(IOAPersonSettingService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取类型列表
        /// </summary>  
        /// <returns>组织架构树</returns>
        [HttpGet]
        public async Task<IEnumerable<OAPersonSettingDto>> GetListAsync()
        {
            return await _service.GetListAsync();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        [HttpPost]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> AddAsync([FromBody] OAPersonSettingForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("分组名已被使用");
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
        public async Task<BaseMessage> UpdateAsync([FromBody] OAPersonSettingForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("分组名已被使用");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">组织id</param>
        /// <returns>结果</returns>
        [HttpDelete("{id}")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> DeleteAsync(Guid id)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(id);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataExist: return msg.Fail("该分组设置不允许操作删除");
                default: return msg.Fail("删除失败");
            }
        }

        /// <summary>
        /// 重新排序
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("Batch/SortNumber")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> SortAsync([FromBody] IEnumerable<Guid> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.SortAsync(ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("操作成功");
                default: return msg.Fail("操作失败");
            }
        }

        #region 字段信息

        /// <summary>
        /// 添加字段
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="entity">字段表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [Route("{id}/Fields")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> AddFieldAsync(Guid id, [FromBody] OAPersonSettingFieldForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddFieldAsync(id, entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("字段名已被使用");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="entity">字段表单</param>
        /// <returns>结果</returns>
        [HttpPut]
        [Route("{id}/Fields")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> UpdateFieldAsync(Guid id, [FromBody] OAPersonSettingFieldForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateFieldAsync(id, entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("字段名已被使用");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 启用字段
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="fieldId">字段id</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("{id}/Fields/{fieldId}/IsEnabled")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> EnableFieldAsync(Guid id, Guid fieldId)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.EnableFieldAsync(id, fieldId);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("设置成功");
                case BaseErrType.DataExist: return msg.Fail("该字段不允许修改启用状态");
                default: return msg.Fail("设置失败");
            }
        }

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="fieldId">字段id</param>
        /// <returns>结果</returns>
        [HttpDelete]
        [Route("{id}/Fields/{fieldId}")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> DeleteFieldAsync(Guid id, Guid fieldId)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteFieldAsync(id, fieldId);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataExist: return msg.Fail("该字段不允许删除");
                default: return msg.Fail("删除失败");
            }
        }

        /// <summary>
        /// 重新排序字段
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="fieldIds">字段id</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("{id}/Fields/Batch/SortNumber")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> SortFieldAsync(Guid id, [FromBody] IEnumerable<Guid> fieldIds)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.SortFieldAsync(id, fieldIds);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("操作成功");
                case BaseErrType.DataNotFound: return msg.Fail("没有可以排序的分组");
                default: return msg.Fail("操作失败");
            }
        }
        #endregion
    }
}
