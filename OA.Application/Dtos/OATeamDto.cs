using System;
using System.Text;
using System.Collections.Generic;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 企业组织架构
    /// </summary>
    public class OATeamDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 上级名称
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 直属主管id
        /// </summary>
        public Guid LeaderId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
