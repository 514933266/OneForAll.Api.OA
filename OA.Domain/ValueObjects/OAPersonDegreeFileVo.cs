using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人材料设置-学位证书
    /// </summary>
    public class OAPersonDegreeFileVo : OAPersonDefaultFieldVo
    {
        public OAPersonDegreeFileVo()
        {
            Name = "DegreeFile";
            Text = "学位证书";
            Placeholder = "请上传学位证书";
            IsDefault = false;
            IsRequired = false;
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

