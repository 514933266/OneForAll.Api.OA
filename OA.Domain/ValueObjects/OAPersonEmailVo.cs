using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案基础信息设置-邮箱
    /// </summary>
    public class OAPersonEmailVo : OAPersonDefaultFieldVo
    {
        public OAPersonEmailVo()
        {
            Name = "Email";
            Text = "邮箱";
            Placeholder = "请填写有效电子邮箱";
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            IsEnableEmployeeVisiable = true;
            IsEnableEmployeeEditable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
