using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Models
{
    /// <summary>
    /// 人员档案设置
    /// </summary>
    public class OAPersonSettingForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Text { get; set; }

        /// <summary>
        /// 是否分组（分组即可填写多栏信息）
        /// </summary>
        [Required]
        public bool IsGrouped { get; set; }

        public virtual ICollection<OAPersonSettingFieldForm> Fields { get; set; }
    }
}
