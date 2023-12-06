using OA.Domain.AggregateRoots;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Aggregates
{
    /// <summary>
    /// 人员档案（基础）
    /// </summary>
    public class OAPersonBasicInfoAggr : Entity<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// 直属团队
        /// </summary>
        public OATeam Team { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="entity">人员档案</param>
        public void Init(OAPerson entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Sex = entity.Sex;
            Age = entity.Age;
            IconUrl = entity.IconUrl;
        }
    }
}
