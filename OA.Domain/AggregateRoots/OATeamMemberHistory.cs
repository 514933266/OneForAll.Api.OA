using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OneForAll.Core.DDD;
using OA.Domain.Enums;
using OneForAll.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 基础表：团队成员调动历史记录
    /// </summary>
    public class OATeamMemberHistory : AggregateRoot<Guid>, ICreateTime
    {
        /// <summary>
        /// 人员id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// 人员id
        /// </summary>
        [Required]
        public Guid OAPersonId { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string PersonName { get; set; }

        /// <summary>
        /// 人员职级
        /// </summary>
        [Required]
        [StringLength(20)]
        public string PersonJob { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        [Required]
        public Guid OATeamId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string TeamName { get; set; } = "";

        /// <summary>
        /// 目标部门id
        /// </summary>
        [Required]
        public Guid TargetOATeamId { get; set; }

        /// <summary>
        /// 目标部门名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string TargetTeamName { get; set; } = "";

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public OATeamMemberTransferEnum Type { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        [Required]
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        [Required]
        [StringLength(20)]
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }
    }
}