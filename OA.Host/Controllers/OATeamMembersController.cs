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

namespace OA.Host.Controllers
{
    /// <summary>
    /// 团队成员
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.PUBLIC)]
    public class OATeamMembersController : BaseController
    {
        private readonly IOATeamMemberService _service;
        public OATeamMembersController(IOATeamMemberService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="teamId">团队id，当为默认值的时候返回全部团队的人员信息</param>
        /// <param name="deep">是否递归</param>
        /// <returns>结果</returns>
        [HttpGet]
        public async Task<IEnumerable<OATeamMemberDto>> GetListAsync([FromQuery] Guid teamId, [FromQuery] bool deep)
        {
            return await _service.GetListAsync(teamId, deep);
        }

        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="member">人员</param>
        /// <returns>结果</returns>
        [HttpPost]
        [CheckPermission(Controller = "OATeam", Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> AddAsync([FromBody] OATeamMemberForm member)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(member);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加部门成员成功");
                case BaseErrType.DataExist: return msg.Success("成员已加入部门");
                default: return msg.Fail("添加部门成员失败");
            }
        }

        /// <summary>
        /// 修改成员
        /// </summary>
        /// <param name="member">人员</param>
        /// <returns>结果</returns>
        [HttpPut]
        [CheckPermission(Controller = "OATeam", Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> UpdateAsync([FromBody] OATeamMemberForm member)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(member);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成员资料成功");
                default: return msg.Fail("修改成员资料失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("Batch/IsDeleted")]
        [CheckPermission(Controller = "OATeam", Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> DeleteAsync([FromBody] OATeamMemberDeleteForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success(form.IsLeave ? "成员离职成功" : "删除成员成功");
                case BaseErrType.DataEmpty: return msg.Fail("请选择成员");
                default: return msg.Fail(form.IsLeave ? "成员离职失败" : "删除成员失败");
            }
        }

        /// <summary>
        /// 调动部门
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("Batch/TeamId")]
        [CheckPermission(Controller = "OATeam", Action = ConstPermission.EnterView)]
        public async Task<BaseMessage> TransferAsync([FromBody] OATeamMemberTransferForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.TransferAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("人员调动成功");
                case BaseErrType.DataEmpty: return msg.Fail("请选择人员");
                case BaseErrType.DataError: return msg.Fail("数据异常");
                case BaseErrType.DataNotFound: return msg.Fail("请选择要调动的成员");
                default: return msg.Fail("人员调动失败");
            }
        }
    }
}
