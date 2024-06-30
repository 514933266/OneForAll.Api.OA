using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案基础信息设置-手机
    /// </summary>
    public class OAPersonMobilePhoneVo : OAPersonDefaultFieldVo
    {
        public OAPersonMobilePhoneVo()
        {
            Name = "MobilePhone";
            Text = "手机号";
            Placeholder = "请填写手机号码";
            IsDefault = true;
            IsRequired = true;
            IsEntryFileVisiable = true;
            IsEmployeeVisiable = true;
            IsEnableRequired = true;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
