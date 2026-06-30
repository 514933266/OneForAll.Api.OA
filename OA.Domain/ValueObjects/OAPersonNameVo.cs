using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案基础信息设置-姓名
    /// </summary>
    public class OAPersonNameVo : OAPersonDefaultFieldVo
    {
        public OAPersonNameVo()
        {
            Name = "Name";
            Text = "姓名";
            Placeholder = "请填写真实姓名";
            IsDefault = true;
            IsEnabled = true;
            IsRequired = true;
            IsEmployeeVisiable = true;
            IsEntryFileVisiable = true;
        }
    }
}
