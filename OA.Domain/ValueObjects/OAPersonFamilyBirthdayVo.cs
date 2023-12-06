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
            IsDefault = false;
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            Type = Enums.OAPersonSettingFieldTypeEnum.Date;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}

