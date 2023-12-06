using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人材料设置-身份证(国徽面)
    /// </summary>
    public class OAPersonIdCardFile2 : OAPersonDefaultFieldVo
    {
        public OAPersonIdCardFile2()
        {
            Name = "IdCardFile2";
            Text = "身份证(国徽面)";
            Placeholder = "请上传身份证(国徽面)";
            IsRequired = true;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            Type = Enums.OAPersonSettingFieldTypeEnum.File;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}

