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
    /// 企业组织架构
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class OATeamsController : BaseController
    {
        private readonly IOATeamService _teamService;
        public OATeamsController(IOATeamService teamService)
        {
            _teamService = teamService;
        }

        /// <summary>
        /// 获取组织架构树
        /// </summary>
        /// <param name="parentId">上级id</param>
        /// <param name="type">类型</param>
        /// <param name="deep">是否深度检索</param>
        /// <param name="scope">范围 -1全部 0有效 1被删除数据</param>
        /// <returns>组织架构树</returns>
        [HttpGet]
        public async Task<IEnumerable<OATeamTreeDto>> GetListAsync(
            [FromQuery] Guid parentId = default,
            [FromQuery] string type = default,
            [FromQuery] bool deep = false,
            [FromQuery] OATeamSearchScopeEnum scope = OATeamSearchScopeEnum.None)
        {
            return await _teamService.GetListAsync(parentId, type, deep, scope);
        }

        /// <summary>
        /// 获取指定组织
        /// </summary>
        /// <param name="id">组织id</param>
        /// <returns>组织实体</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<OATeamDto> GetAsync(Guid id)
        {
            return await _teamService.GetAsync(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        [HttpPost]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> AddAsync([FromBody] OATeamForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _teamService.AddAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("名称已被使用");
                case BaseErrType.DataNotFound: return msg.Fail("找不到上级分店信息");
                case BaseErrType.NotAllow: return msg.Fail("上级分类不允许创建子级节点");
                case BaseErrType.LowLevel: return msg.Fail("不能选择的组织类型");
                case BaseErrType.DataNotMatch: return msg.Fail("管理人已是其他组织架构的成员");
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
        public async Task<BaseMessage> UpdateAsync([FromBody] OATeamForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _teamService.UpdateAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("名称已被使用");
                case BaseErrType.DataNotFound: return msg.Fail("找不到上级分店信息");
                case BaseErrType.NotAllow: return msg.Fail("上级分类不允许创建子级节点");
                case BaseErrType.LowLevel: return msg.Fail("不能选择的组织类型");
                case BaseErrType.DataNotMatch: return msg.Fail("管理人已是其他组织架构的成员");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">组织id</param>
        /// <param name="isTrue">是否真删除</param>
        /// <returns>结果</returns>
        [HttpDelete("{id}")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> DeleteAsync(Guid id, [FromQuery] bool isTrue = false)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _teamService.DeleteAsync(id, isTrue);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("操作成功");
                case BaseErrType.NotAllow: return msg.Fail("请确保部门没有子级或人员");
                case BaseErrType.DataError: return msg.Fail("数据异常");
                default: return msg.Fail("操作失败");
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="ids">排序表单（以此从小到大排列）</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("Batch/SortNumber")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> SortAsync([FromBody] IEnumerable<Guid> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _teamService.SortAsync(ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("操作成功");
                default: return msg.Fail("操作失败");
            }
        }

        /// <summary>
        /// 添加成员（批量）
        /// </summary>
        /// <param name="id">部门id</param>
        /// <param name="personIds">人员id</param>
        /// <returns>结果</returns>
        [HttpPost]
        [Obsolete]
        [Route("{id}/Members")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> AddMemberAsync(Guid id, [FromBody] IEnumerable<Guid> personIds)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _teamService.AddMemberAsync(id, personIds);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加部门成员成功");
                case BaseErrType.DataEmpty: return msg.Fail("成员已存在目标部门中");
                default: return msg.Fail("添加部门成员失败");
            }
        }
    }
}

