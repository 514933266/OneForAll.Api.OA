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
            Text = "转正日期";
            Placeholder = "请选择实际转正日期";
            Type = Enums.OAPersonSettingFieldTypeEnum.Date;
            IsDefault = true;
            IsEnableRequired = true;
            IsShowEnabled = true;
            IsEnableEntryFileVisiable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEmployeeEditable = true;
        }
    }
}
