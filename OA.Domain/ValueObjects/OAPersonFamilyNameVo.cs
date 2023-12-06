using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案家庭信息设置-姓名(家人)
    /// </summary>
    public class OAPersonFamilyNameVo : OAPersonDefaultFieldVo
    {
        public OAPersonFamilyNameVo()
        {
            Name = "FamilyName";
            Text = "姓名(家人)";
            Placeholder = "请填写姓名(家人)";
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
