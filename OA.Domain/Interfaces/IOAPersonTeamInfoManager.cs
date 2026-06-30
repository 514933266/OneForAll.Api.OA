using OA.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 人员团队信息
    /// </summary>
    public interface IOAPersonTeamInfoManager
    {
        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="teams">团队</param>
        /// <param name="key">人员姓名</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OAPersonTeamInfoAggr>> GetListAsync(IEnumerable<OATeamTreeAggr> teams, string key);

        /// <summary>
        /// 获取人员
        /// </summary>
        /// <param name="teams">团队</param>
        /// <param name="personId">人员id</param>
        /// <returns>人员</returns>
        Task<OAPersonTeamInfoAggr> GetAsync(IEnumerable<OATeamTreeAggr> teams, Guid personId);
    }
}
