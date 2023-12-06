using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OA.Domain.Models
{
    /// <summary>
    /// 表单：团队类型
    /// </summary>
    public class OATeamTypeForm
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        /// <summary>
        /// 是否允许添加子级
        /// </summary>
        public bool CanAddChild { get; set; }
    }
}
