using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人材料设置-员工照片
    /// </summary>
    public class OAPersonIconFileVo : OAPersonDefaultFieldVo
    {
        public OAPersonIconFileVo()
        {
            Name = "IconFile";
            Text = "员工照片";
            Placeholder = "请上传员工照片";
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