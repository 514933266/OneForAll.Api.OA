using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-毕业院校
    /// </summary>
    public class OAPersonSchoolVo : OAPersonDefaultFieldVo
    {
        public OAPersonSchoolVo()
        {
            Name = "School";
            Text = "毕业院校";
            Placeholder = "请填写毕业院校";
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
