using OA.Domain.Enums;
using OA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 人员档案设置字段
    /// </summary>
    public class OAPersonSettingFieldDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 是否启用文本（不启用无法编辑）
        /// </summary>
        public bool IsEnableText { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public OAPersonSettingFieldTypeEnum Type { get; set; }

        /// <summary>
        /// 是否启用类型（不启用无法编辑）
        /// </summary>
        public bool IsEnableType { get; set; }

        /// <summary>
        /// 针对类型的Json配置
        /// </summary>
        public IEnumerable<OAPersonSettingFieldTypeDetailVo> TypeDetails { get; set; }

        /// <summary>
        /// 是否启用类型添加（不启用无法添加或删除）
        /// </summary>
        public bool IsEnableAddTypeDetail { get; set; }

        /// <summary>
        /// 是否默认字段
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否显示启用
        /// </summary>
        public bool IsShowEnabled { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 是否启用必填（不启用无法编辑）
        /// </summary>
        public bool IsEnableRequired { get; set; }

        /// <summary>
        /// 员工可编辑
        /// </summary>
        public bool IsEmployeeEditable { get; set; }

        /// <summary>
        /// 是否启用员工可编辑（不启用无法编辑）
        /// </summary>
        public bool IsEnableEmployeeEditable { get; set; }

        /// <summary>
        /// 员工可见
        /// </summary>
        public bool IsEmployeeVisiable { get; set; }

        /// <summary>
        /// 是否启用显示员工可见（不启用无法编辑）
        /// </summary>
        public bool IsEnableEmployeeVisiable { get; set; }

        /// <summary>
        /// 是否启用员工入职登记表显示
        /// </summary>
        public bool IsEntryFileVisiable { get; set; }

        /// <summary>
        /// 是否启用员工入职登记表显示（不启用无法编辑）
        /// </summary>
        public bool IsEnableEntryFileVisiable { get; set; }

        /// <summary>
        /// 水印提示
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// 字段提示
        /// </summary>
        public string Tips { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNumber { get; set; }
    }
}
