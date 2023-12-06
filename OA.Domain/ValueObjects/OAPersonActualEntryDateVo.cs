using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案工作信息设置-实际转正日期
    /// </summary>
    public class OAPersonActualEntryDateVo : OAPersonDefaultFieldVo
    {
        public OAPersonActualEntryDateVo()
        {
            Name = "ActualEntryDate";
            Text = "实际转正日期";
            Placeholder = "请选择实际转正日期";
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            Type = Enums.OAPersonSettingFieldTypeEnum.Date;
            IsEnableEmployeeEditable = false;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = false;
            IsEnableEntryFileVisiable = false;
        }
    }
}
