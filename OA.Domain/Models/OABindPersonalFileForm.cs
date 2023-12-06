using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Models
{
    /// <summary>
    /// 绑定个人档案
    /// </summary>
    public class OABindPersonalFileForm
    {
        /// <summary>
        /// 身份证 或 手机号码
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Key { get; set; }
    }
}
