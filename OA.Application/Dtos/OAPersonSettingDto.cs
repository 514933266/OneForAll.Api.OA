using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 人员档案设置
    /// </summary>
    public class OAPersonSettingDto
    {
        public OAPersonSettingDto()
        {
            Fields = new List<OAPersonSettingFieldDto>();
        }

        public Guid Id { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 类型（用来识别系统默认模板）
        /// </summary>
        public OAPersonSettingTypeEnum Type { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        public int SortNumber { get; set; }

        /// <summary>
        /// 是否可排序
        /// </summary>
        public bool IsSortable { get; set; }

        /// <summary>
        /// 是否默认(默认不可删除)
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否分组（分组即可填写多栏信息）
        /// </summary>
        public bool IsGrouped { get; set; }

        /// <summary>
        /// 是否显示可分组（分组即可填写多栏信息）
        /// </summary>
        public bool IsShowGrouped { get; set; }

        /// <summary>
        /// 字段明细
        /// </summary>
        public List<OAPersonSettingFieldDto> Fields { get; set; }
    }
}
