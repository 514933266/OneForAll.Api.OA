using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-专业
    /// </summary>
    public class OAPersonMajorVo : OAPersonDefaultFieldVo
    {
        public OAPersonMajorVo()
        {
            Name = "Major";
            Text = "专业";
            Placeholder = "请填写专业";
            IsDefault = false;
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
