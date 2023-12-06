using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Enums
{
    /// <summary>
    /// 人员档案设置字段类型
    /// </summary>
    public enum OAPersonSettingFieldTypeEnum
    {
        /// <summary>
        /// 无
        /// </summary>
        None = -1,

        /// <summary>
        /// 文本
        /// </summary>
        Text = 0,

        /// <summary>
        /// 文本域
        /// </summary>
        Textarea = 1,

        /// <summary>
        /// 富文本
        /// </summary>
        RichText = 2,

        /// <summary>
        /// 日期
        /// </summary>
        Date = 3,

        /// <summary>
        /// 日期（含时间）
        /// </summary>
        DateTime = 4,

        /// <summary>
        /// 选项
        /// </summary>
        Select = 5,

        /// <summary>
        /// Radio选项
        /// </summary>
        Radio = 6,

        /// <summary>
        /// 附件
        /// </summary>
        File = 7,

        /// <summary>
        /// CheckBox选项
        /// </summary>
        CheckBox = 8
    }
}
