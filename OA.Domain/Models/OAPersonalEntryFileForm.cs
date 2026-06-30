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
    /// 员工入职资料填写
    /// </summary>
    public class OAPersonalEntryFileForm
    {
        /// <summary>
        /// 数据Id
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        [Required]
        public IEnumerable<OAPersonExtenInformationFieldVo> ExtendInformations { get; set; } = new HashSet<OAPersonExtenInformationFieldVo>();
    }
}
