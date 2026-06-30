using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 未加入团队成员信息
    /// </summary>
    public class OANoTeamPersonDto
    {
        /// <summary>
        /// 人员id
        /// </summary>
        public Guid Id { get; set; }

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
        /// 入职日期
        /// </summary>
        public DateTime EntryDate { get; set; }
    }
}
