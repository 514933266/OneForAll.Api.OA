using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Models
{
    /// <summary>
    /// 自定义表格字段显示配置
    /// </summary>
    public class OATableColumnSettingForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        [Required]
        public OATableColumnSettingsTargetEnum Target { get; set; }

        /// <summary>
        /// 可见字段值Json
        /// </summary>
        [Required]
        public IEnumerable<string> VisiableFields { get; set; }
    }
}
