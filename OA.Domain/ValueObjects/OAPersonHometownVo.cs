using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-家乡
    /// </summary>
    public class OAPersonHometownVo : OAPersonDefaultFieldVo
    {
        public OAPersonHometownVo()
        {
            Name = "Hometown";
            Text = "家乡";
            Placeholder = "请填写家乡";
            IsRequired = true;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}

