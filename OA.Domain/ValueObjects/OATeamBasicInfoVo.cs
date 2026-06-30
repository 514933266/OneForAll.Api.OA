using OA.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 值对象：团队资料
    /// </summary>
    public class OATeamBasicInfoVo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 主管id
        /// </summary>
        public Guid LeaderId { get; set; }

        /// <summary>
        /// 主管名称
        /// </summary>
        public string LeaderName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public void Init(OATeam entity)
        {
            Name = entity.Name;
            Type = entity.Type;
        }
    }
}
