using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 自定义表格字段显示配置
    /// </summary>
    public class OATableColumnSettingDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        public OATableColumnSettingsTargetEnum Target { get; set; }

        /// <summary>
        /// 可见列
        /// </summary>
        public IEnumerable<string> VisiableFields { get; set; }
    }
}
