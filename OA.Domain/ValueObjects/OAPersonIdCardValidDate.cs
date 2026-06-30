using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-身份证有效期
    /// </summary>
    public class OAPersonIdCardValidDate : OAPersonDefaultFieldVo
    {
        public OAPersonIdCardValidDate()
        {
            Name = "IdCardValidDate";
            Text = "身份证有效期";
            Placeholder = "请填写身份证有效期";
            Type = Enums.OAPersonSettingFieldTypeEnum.Date;
            IsDefault = true;
            IsRequired = true;
            IsShowEnabled = true;
            IsEnableRequired = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
        }
    }
}
