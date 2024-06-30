using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案紧急联系人设置-联系人电话
    /// </summary>
    public class OAPersonContactMobilePhoneVo : OAPersonDefaultFieldVo
    {
        public OAPersonContactMobilePhoneVo()
        {
            Name = "ContactMobilePhone";
            Text = "联系人电话";
            Placeholder = "请填写联系人电话";
            IsDefault = true;
            IsRequired = true;
            IsEntryFileVisiable = true;
            IsEnableText = true;
            IsEnableRequired = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
            IsEnableEmployeeEditable = true;
        }
    }
}
