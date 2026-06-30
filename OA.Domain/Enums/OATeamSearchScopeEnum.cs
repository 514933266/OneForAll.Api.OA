using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Enums
{
    /// <summary>
    /// 团队搜索范围
    /// </summary>
    public enum OATeamSearchScopeEnum
    {
        /// <summary>
        /// 无
        /// </summary>
        None = -1,

        /// <summary>
        /// 有效数据
        /// </summary>
        Valid = 0,

        /// <summary>
        /// 被撤销的数据
        /// </summary>
        InValid = 1
    }
}
