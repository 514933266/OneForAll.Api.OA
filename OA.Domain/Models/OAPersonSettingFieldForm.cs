using OA.Domain.Enums;
using OA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Models
{
    /// <summary>
    /// 人员档案设置字段
    /// </summary>
    public class OAPersonSettingFieldForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Text { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public OAPersonSettingFieldTypeEnum Type { get; set; }

        /// <summary>
        /// 针对类型的Json配置
        /// </summary>
        public IEnumerable<OAPersonSettingFieldTypeDetailVo> TypeDetails { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 员工可编辑
        /// </summary>
        public bool IsEmployeeEditable { get; set; }

        /// <summary>
        /// 员工可见
        /// </summary>
        public bool IsEmployeeVisiable { get; set; }

        /// <summary>
        /// 是否启用员工入职登记表显示
        /// </summary>
        [Required]
        public bool IsEntryFileVisiable { get; set; }

        /// <summary>
        /// 水印提示
        /// </summary>
        [StringLength(50)]
        public string Placeholder { get; set; }
    }
}
