using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人材料设置-离职证明
    /// </summary>
    public class OAPersonResignationFileVo : OAPersonDefaultFieldVo
    {
        public OAPersonResignationFileVo()
        {
            Name = "ResignationFile";
            Text = "离职证明";
            Placeholder = "请上传离职证明";
            IsDefault = false;
            IsRequired = true;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            IsEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            Type = Enums.OAPersonSettingFieldTypeEnum.File;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
