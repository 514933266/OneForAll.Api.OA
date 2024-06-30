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
        public OAPersonEmployeeTypeVo()
        {
            Name = "EmployeeType";
            Text = "员工类型";
            Placeholder = "请选择员工类型";
            Type = OAPersonSettingFieldTypeEnum.Select;
            Value = "全职";
            TypeDetail = new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = "全职", Value = "全职" },
                new OAPersonDefaultSelectVo() { Name = "实习生", Value = "实习生" },
                new OAPersonDefaultSelectVo() { Name = "兼职", Value = "兼职" },
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
