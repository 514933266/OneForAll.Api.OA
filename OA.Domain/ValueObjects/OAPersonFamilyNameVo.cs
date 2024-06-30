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
