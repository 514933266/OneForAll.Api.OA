using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 企业组织架构树
    /// </summary>
    public class OATeamTreeDto : IParent<Guid>, IEntity<Guid>, IChildren<OATeamTreeDto>
    {

        public Guid Id { get; set; }

        /// <summary>
        /// 上级名称
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 直属主管Id
        /// </summary>
        public Guid LeaderId { get; set; }

        /// <summary>
        /// 直属主管姓名
        /// </summary>
        public string LeaderName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 成员数量
        /// </summary>
        public int MemberNumber { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 子级
        /// </summary>

        public IEnumerable<OATeamTreeDto> Children { get; set; }

    }
}
