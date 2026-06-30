using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Enums
{
    /// <summary>
    /// 团队成员调动类型
    /// </summary>

    public enum OATeamMemberTransferEnum
    {
        /// <summary>
        /// 无
        /// </summary>
        None = -1,

        /// <summary>
        /// 普通
        /// </summary>
        Normal,

        /// <summary>
        /// 升级
        /// </summary>
        Upgrade,

        /// <summary>
        /// 降级
        /// </summary>
        Downgrade,

        /// <summary>
        /// 加入
        /// </summary>
        Join,

        /// <summary>
        /// 离职
        /// </summary>
        Leave
    }
}
