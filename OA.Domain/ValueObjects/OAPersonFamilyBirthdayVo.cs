using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案家庭信息设置-生日(家人)
    /// </summary>
    public class OAPersonFamilyBirthdayVo : OAPersonDefaultFieldVo
    {
        public OAPersonFamilyBirthdayVo()
        {
            Name = "FamilyBirthday";
            Text = "生日(家人)";
            Placeholder = "请选择生日(家人)";
            Type = Enums.OAPersonSettingFieldTypeEnum.Date;
            IsShowEnabled = true;
            IsEntryFileVisiable = true;
            IsEnableText = true;
            IsEnableRequired = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
            IsEnableEmployeeEditable = true;
        }
    }
}

