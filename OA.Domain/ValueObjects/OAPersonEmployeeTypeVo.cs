using OA.Domain.Enums;
using OneForAll.Core.Extension;
using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案工作信息设置-员工类型
    /// </summary>
    public class OAPersonEmployeeTypeVo : OAPersonDefaultFieldVo
    {
        /// <summary>
        /// 全职
        /// </summary>
        public const string ValueFullTime = "全职";

        /// <summary>
        /// 实习生
        /// </summary>
        public const string ValueIntern = "实习生";

        /// <summary>
        /// 兼职
        /// </summary>
        public const string ValuePartTime = "兼职";

        public OAPersonEmployeeTypeVo()
        {
            Name = "EmployeeType";
            Text = "员工类型";
            Placeholder = "请选择员工类型";
            Type = OAPersonSettingFieldTypeEnum.Select;
            Value = ValueFullTime;
            TypeDetail = new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = ValueFullTime, Value = ValueFullTime },
                new OAPersonDefaultSelectVo() { Name = ValueIntern, Value = ValueIntern },
                new OAPersonDefaultSelectVo() { Name = ValuePartTime, Value = ValuePartTime },
                new OAPersonDefaultSelectVo() { Name = "劳务派遣", Value = "劳务派遣" },
                new OAPersonDefaultSelectVo() { Name = "退休返聘", Value = "退休返聘" }
            }.ToJson();
            IsDefault = true;
            IsShowEnabled = true;
            IsEnableText = true;
            IsEnableRequired = true;
            IsEnableEmployeeVisiable = true;
            IsEnableAddTypeDetail = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
