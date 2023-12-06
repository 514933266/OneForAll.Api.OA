using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OA.Domain.Models
{
    /// <summary>
    /// 表单：团队排序
    /// </summary>
    public class OATeamSortForm : Entity<Guid>
    {
        /// <summary>
        /// 排序号 由大到小
        /// </summary>
        [Required]
        public int SortNumber { get; set; }
    }
}
