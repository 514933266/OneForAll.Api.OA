using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 团队成员
    /// </summary>
    public class OATeamMemberDto
    {
        /// <summary>
        /// 人员id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 是否管理者
        /// </summary>
        public bool IsLeader { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// 是否已离职
        /// </summary>
        public bool IsLeave { get; set; }

        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
