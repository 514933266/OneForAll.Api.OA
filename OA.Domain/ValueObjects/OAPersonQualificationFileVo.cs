using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人材料设置-学历证书
    /// </summary>
    public class OAPersonQualificationFileVo : OAPersonDefaultFieldVo
    {
        public OAPersonQualificationFileVo()
        {
            Name = "QualificationFile";
            Text = "学历证书";
            Placeholder = "请上传学历证书";
            IsDefault = false;
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