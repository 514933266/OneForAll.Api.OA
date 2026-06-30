using OA.Domain.Enums;
using OA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 职位管理
    /// </summary>
    public class OAJobDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类别id
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 职级id
        /// </summary>
        public Guid LevelId { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        public string LevelName { get; set; }

        /// <summary>
        /// 职务id
        /// </summary>
        public Guid DutyId { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string DutyName { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// 团队
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

    }
}
