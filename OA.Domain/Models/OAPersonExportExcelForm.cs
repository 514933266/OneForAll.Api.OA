using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Models
{
    /// <summary>
    /// 导出人员档案
    /// </summary>
    public class OAPersonExportExcelForm
    {
        /// <summary>
        /// 在职状态
        /// </summary>
        public OAPersonOnJobStatusEnum OnJobStatus { get; set; }

        /// <summary>
        /// 员工类型
        /// </summary>
        public string EmployeeType { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        public string EmployeeStatus { get; set; }

        /// <summary>
        /// 需要导出的字段
        /// </summary>
        public List<string> Fields { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        public List<string> Jobs { get; set; }

        /// <summary>
        /// 开始入职时间
        /// </summary>
        public DateTime? StartEntryDate { get; set; }

        /// <summary>
        /// 结束入职时间
        /// </summary>
        public DateTime? EndEntryDate { get; set; }
    }
}
