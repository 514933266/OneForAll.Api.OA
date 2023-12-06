using OA.Domain.Enums;
using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 表格自定义字段显示配置
    /// </summary>
    public class OATableColumnSetting : AggregateRoot<Guid>
    {
        /// <summary>
        /// 所属机构id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        [Required]
        public OATableColumnSettingsTargetEnum Target { get; set; }

        /// <summary>
        /// 可见列值Json
        /// </summary>
        [Required]
        public string VisiableFields { get; set; } = "";

        /// <summary>
        /// 创建人id
        /// </summary>
        [Required]
        public Guid CreatorId { get; set; }
    }
}
