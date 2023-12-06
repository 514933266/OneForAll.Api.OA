using OA.Domain.Enums;
using OA.Domain.Models;
using OA.Domain.ValueObjects;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 人员档案设置字段
    /// </summary>
    public class OAPersonSettingField : AggregateRoot<Guid>
    {
        /// <summary>
        /// 人员档案设置Id
        /// </summary>
        public Guid OAPersonSettingId { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Text { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Value { get; set; } = "";

        /// <summary>
        /// 是否启用文本（不启用无法编辑）
        /// </summary>
        [Required]
        public bool IsEnableText { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public OAPersonSettingFieldTypeEnum Type { get; set; }

        /// <summary>
        /// 是否启用类型（不启用无法编辑）
        /// </summary>
        [Required]
        public bool IsEnableType { get; set; }

        /// <summary>
        /// 针对类型的Json配置
        /// </summary>
        [Required]
        [StringLength(2000)]
        public string TypeDetail { get; set; } = "";

        /// <summary>
        /// 是否启用类型添加（不启用无法添加或删除）
        /// </summary>
        [Required]
        public bool IsEnableAddTypeDetail { get; set; }

        /// <summary>
        /// 是否默认字段
        /// </summary>
        [Required]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否显示启用
        /// </summary>
        [Required]
        public bool IsShowEnabled { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        [Required]
        public bool IsRequired { get; set; }

        /// <summary>
        /// 是否启用必填（不启用无法编辑）
        /// </summary>
        [Required]
        public bool IsEnableRequired { get; set; }

        /// <summary>
        /// 员工可编辑
        /// </summary>
        [Required]
        public bool IsEmployeeEditable { get; set; }

        /// <summary>
        /// 是否启用员工可编辑（不启用无法编辑）
        /// </summary>
        [Required]
        public bool IsEnableEmployeeEditable { get; set; }

        /// <summary>
        /// 员工可见
        /// </summary>
        [Required]
        public bool IsEmployeeVisiable { get; set; }

        /// <summary>
        /// 是否启用显示员工可见（不启用无法编辑）
        /// </summary>
        [Required]
        public bool IsEnableEmployeeVisiable { get; set; }

        /// <summary>
        /// 是否启用员工入职登记表显示
        /// </summary>
        [Required]
        public bool IsEntryFileVisiable { get; set; }

        /// <summary>
        /// 是否启用员工入职登记表显示（不启用无法编辑）
        /// </summary>
        [Required]
        public bool IsEnableEntryFileVisiable { get; set; }

        /// <summary>
        /// 水印提示
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Placeholder { get; set; } = "";

        /// <summary>
        /// 字段提示
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Tips { get; set; } = "";

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int SortNumber { get; set; }

        public bool Update(OAPersonSettingFieldForm entity)
        {
            if (IsEnableText) Text = entity.Text;
            if (IsEnableType) Type = entity.Type;
            if (IsEnableRequired) IsRequired = entity.IsRequired;
            if (IsEnableAddTypeDetail) TypeDetail = entity.TypeDetails.ToJson();
            if (IsEnableEmployeeEditable) IsEmployeeEditable = entity.IsEmployeeEditable;
            if (IsEnableEmployeeVisiable) IsEmployeeVisiable = entity.IsEmployeeVisiable;
            Text = entity.Text;
            Placeholder = entity.Placeholder;
            TypeDetail = Type == OAPersonSettingFieldTypeEnum.Select ? TypeDetail : "";
            IsEnableAddTypeDetail = Type == OAPersonSettingFieldTypeEnum.Select ? true : false;
            return true;
        }
    }
}
