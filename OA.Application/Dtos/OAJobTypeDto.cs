using OA.Domain.Enums;
using OA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 职级类型
    /// </summary>
    public class OAJobTypeDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
