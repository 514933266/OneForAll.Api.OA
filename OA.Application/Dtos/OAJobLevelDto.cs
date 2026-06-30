using OA.Domain.Enums;
using OA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 职级管理
    /// </summary>
    public class OAJobLevelDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public decimal BaseSalary { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
