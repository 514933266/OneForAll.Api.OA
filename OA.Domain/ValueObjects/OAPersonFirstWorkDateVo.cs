using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-首次参加工作时间
    /// </summary>
    public class OAPersonFirstWorkDateVo : OAPersonDefaultFieldVo
    {
        public OAPersonFirstWorkDateVo()
        {
            Name = "FirstWorkDate";
            Text = "首次参加工作时间";
            Placeholder = "请选择首次参加工作时间";
            Type = OAPersonSettingFieldTypeEnum.Date;
            IsEnableText = true;
            IsShowEnabled = true;
            IsEnableRequired = true;
            IsEntryFileVisiable = true;
            IsEmployeeVisiable = true;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
