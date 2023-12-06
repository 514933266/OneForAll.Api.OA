using OA.Application.Dtos;
using OA.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 团队成员
    /// </summary>
    public interface IOATeamMemberService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="teamId">团队id，当为默认值的时候返回没有团队的人员信息</param>
        /// <param name="deep">是否递归</param>
        /// <returns>结果</returns>
        Task<IEnumerable<OATeamMemberDto>> GetListAsync(Guid teamId, bool deep);

        /// <summary>
        /// 获取未加入团队列表
        /// </summary>
        /// <returns>成员列表</returns>
        Task<IEnumerable<OANoTeamPersonDto>> GetListNoTeamAsync();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="member">成员</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OATeamMemberForm member);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="member">成员</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OATeamMemberForm member);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(OATeamMemberDeleteForm form);

        /// <summary>
        /// 调动部门
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> TransferAsync(OATeamMemberTransferForm form);
    }
}
