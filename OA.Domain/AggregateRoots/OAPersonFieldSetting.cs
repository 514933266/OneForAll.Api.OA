using OA.Domain.Enums;
using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 人员档案字段设置
    /// </summary>
    public class OAPersonFieldSetting : AggregateRoot<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public OAPersonSettingFieldTypeEnum Type { get; set; }

        /// <summary>
        /// 水印提示
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Placeholder { get; set; } = "";

        /// <summary>
        /// 排序编号
        /// </summary>
        [Required]
        public int SortNumber { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否默认（默认不可删除）
        /// </summary>
        [Required]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        [Required]
        public bool IsRequired { get; set; }

        /// <summary>
        /// 是否允许员工编辑
        /// </summary>
        [Required]
        public bool IsEnabledEmployeEdit { get; set; }

        /// <summary>
        /// 是否员工可见
        /// </summary>
        [Required]
        public bool IsEmployeVisibled { get; set; }
    }
}
