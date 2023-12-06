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
            IsDefault = false;
            IsRequired = true;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}