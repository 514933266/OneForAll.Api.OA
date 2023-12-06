using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ExcelModels
{
    /// <summary>
    /// 人员资料Excel
    /// </summary>
    [Display(Name = "人员资料")]
    public class OAPersonExcel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [Display(Name = "工号")]
        public string WorkNumber { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        [Display(Name = "职级")]
        public string Job { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 员工类型
        /// </summary>
        [Display(Name = "员工类型")]
        public string EmployeeType { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        [Display(Name = "员工状态")]
        public string EmployeeStatus { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Display(Name = "年龄")]
        public string Age { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [Display(Name = "身份证号")]
        public string IdCard { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Display(Name = "出生日期")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        [Display(Name = "入职日期")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime? EntryDate { get; set; }
    }
}
