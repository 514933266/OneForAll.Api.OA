using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 用户档案
    /// </summary>
    public class OAPersonUserContact
    {
        /// <summary>
        /// 租户id
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 系统用户id
        /// </summary>
        [Required]
        public Guid SysUserId { get; set; }

        /// <summary>
        /// 档案id
        /// </summary>
        [Required]
        public Guid OAPersonId { get; set; }
    }
}
