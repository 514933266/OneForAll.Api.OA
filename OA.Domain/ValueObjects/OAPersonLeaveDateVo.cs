using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案基础信息设置-离职日期
    /// </summary>
    public class OAPersonLeaveDateVo : OAPersonDefaultFieldVo
    {
        public OAPersonLeaveDateVo()
        {
            Name = "LeaveDate";
            Text = "离职日期";
            Placeholder = "请选择离职日期";
            Type = OAPersonSettingFieldTypeEnum.Date;
            IsDefault = true;
            IsEnableRequired = true;
            IsEnableEntryFileVisiable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEmployeeEditable = true;
        }
    }
}
