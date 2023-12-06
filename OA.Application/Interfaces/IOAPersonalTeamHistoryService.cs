using OA.Application.Dtos;
using OA.Public.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 人事历程
    /// </summary>
    public interface IOAPersonalTeamHistoryService
    {
        /// <summary>
        /// 获取人事历程列表
        /// </summary>
        /// <param name="loginUser">登录用户</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberHistoryDto>> GetListAsync(LoginUser loginUser);
    }
}
