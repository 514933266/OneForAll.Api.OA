using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-出生日期
    /// </summary>
    public class OAPersonBirthdayVo : OAPersonDefaultFieldVo
    {
        public OAPersonBirthdayVo()
        {
            Name = "Birthday";
            Text = "出生日期";
            Placeholder = "请填写出生日期";
            Tips = "可根据[身份证]信息自动计算";
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            Type = Enums.OAPersonSettingFieldTypeEnum.Date;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = false;
            IsEnableEntryFileVisiable = true;
        }
    }
}
