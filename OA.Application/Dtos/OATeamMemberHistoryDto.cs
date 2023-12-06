using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 团队成员异动历史
    /// </summary>
    public class OATeamMemberHistoryDto
    {
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// 人员职级
        /// </summary>
        public string PersonJob { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 目标部门名称
        /// </summary>
        public string TargetTeamName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public OATeamMemberTransferEnum Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
