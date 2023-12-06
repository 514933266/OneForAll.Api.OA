using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 未加入团队人员
    /// </summary>
    public interface IOANoTeamPersonManager
    {
        /// <summary>
        /// 查询没有加入部门的在职成员
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<OAPerson>> GetListAsync();
    }
}
