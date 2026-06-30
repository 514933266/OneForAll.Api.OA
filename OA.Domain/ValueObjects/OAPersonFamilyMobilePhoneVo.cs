using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案家庭信息设置-电话(家人)
    /// </summary>
    public class OAPersonFamilyMobilePhoneVo : OAPersonDefaultFieldVo
    {
        public OAPersonFamilyMobilePhoneVo()
        {
            Name = "FamilyMobilePhone";
            Text = "电话(家人)";
            Placeholder = "请填写电话(家人)";
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