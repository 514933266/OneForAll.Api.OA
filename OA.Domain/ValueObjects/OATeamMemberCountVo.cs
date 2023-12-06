using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 团队成员数量
    /// </summary>
    public class OATeamMemberCountVo
    {
        /// <summary>
        /// 团队id
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// 成员数量
        /// </summary>
        public int Count { get; set; }
    }
}
