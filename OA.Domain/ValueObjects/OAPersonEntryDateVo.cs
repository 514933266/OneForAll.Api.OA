using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案基础信息设置-入职日期
    /// </summary>
    public class OAPersonEntryDateVo : OAPersonDefaultFieldVo
    {
        public OAPersonEntryDateVo()
        {
            Name = "EntryDate";
            Text = "入职日期";
            Placeholder = "请选择入职日期";
            IsRequired = true;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            Type = OAPersonSettingFieldTypeEnum.Date;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = false;
            IsEnableEntryFileVisiable = false;
        }
    }
}
