using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人材料设置-身份证(人像面)
    /// </summary>
    public class OAPersonIdCardFile : OAPersonDefaultFieldVo
    {
        public OAPersonIdCardFile()
        {
            Name = "IdCardFile";
            Text = "身份证(人像面)";
            Placeholder = "请上传身份证(人像面)";
            Type = Enums.OAPersonSettingFieldTypeEnum.File;
            IsDefault = true;
            IsRequired = true;
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

