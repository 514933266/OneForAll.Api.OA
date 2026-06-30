using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-身份证
    /// </summary>
    public class OAPersonIdCardVo : OAPersonDefaultFieldVo
    {
        public OAPersonIdCardVo()
        {
            Name = "IdCard";
            Text = "证件号码";
            Placeholder = "请填写证件号码";
            IsDefault = true;
            IsRequired = true;
            IsEnableText = true;
            IsEnableRequired = true;
            IsEntryFileVisiable = true;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
