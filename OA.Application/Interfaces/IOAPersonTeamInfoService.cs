using OA.Application.Dtos;
using OA.Domain.Aggregates;
using OA.Domain.Enums;
using OA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 人员团队信息
    /// </summary>
    public interface IOAPersonTeamInfoService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OAPersonTeamInfoDto>> GetListAsync(string key);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        Task<OAPersonTeamInfoDto> GetAsync(Guid id);
    }
}
