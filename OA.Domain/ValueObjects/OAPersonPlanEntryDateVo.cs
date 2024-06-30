using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案工作信息设置-预估转正日期
    /// </summary>
    public class OAPersonPlanEntryDateVo : OAPersonDefaultFieldVo
    {
        public OAPersonPlanEntryDateVo()
        {
            Name = "PlanEntryDate";
            Text = "计划转正日期";
            Placeholder = "请选择计划转正日期";
            Type = Enums.OAPersonSettingFieldTypeEnum.Date;
            IsEnableRequired = true;
            IsShowEnabled = true;
            IsEnableEntryFileVisiable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEmployeeEditable = true;
        }
    }
}
