using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Models
{
    /// <summary>
    /// OAPerson导出
    /// </summary>
    public class OAPersonExportForm
    {
        /// <summary>
        /// 在职类型
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
        /// 开始入职日期
        /// </summary>
        public DateTime? StartEntryDate { get; set; }

        /// <summary>
        /// 结束入职日期
        /// </summary>
        public DateTime? EndEntryDate { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// 导出字段
        /// </summary>
        public IEnumerable<OAPersonExportFieldForm> Fields { get; set; }
    }

    /// <summary>
    /// OAPerson导出-选择字段明细
    /// </summary>
    public class OAPersonExportFieldForm
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段文本
        /// </summary>
        public string Text { get; set; }
    }
}
