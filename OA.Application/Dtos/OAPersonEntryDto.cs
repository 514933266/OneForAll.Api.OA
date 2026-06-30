using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OA.Domain.Enums;
using OA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 员工入职
    /// </summary>
    public class OAPersonEntryDto
    {
       
        public Guid Id { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// 性别 0女 1男
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 已交入职档案
        /// </summary>
        public bool IsSubmitEntryFile { get; set; }

        /// <summary>
        /// 预计入职时间
        /// </summary>
        public DateTime EstimateEntryDate { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否超期
        /// </summary>
        public bool IsOverdue { get; set; }

        /// <summary>
        /// 超期天数
        /// </summary>
        public int OverdueDays { get; set; }

    }
}
