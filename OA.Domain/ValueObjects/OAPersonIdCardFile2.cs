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
            Type = Enums.OAPersonSettingFieldTypeEnum.File;
            IsDefault = true;
            IsRequired = true;
            IsShowEnabled = true;
            IsEnableRequired = true;
            IsEntryFileVisiable = true;
            IsEnableText = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
            IsEnableEmployeeEditable = true;
        }
    }
}

