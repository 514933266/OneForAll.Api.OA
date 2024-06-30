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
            IsShowEnabled = true;
            IsEntryFileVisiable = true;
            IsEnableRequired = true;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}

