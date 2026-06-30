using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-民族
    /// </summary>
    public class OAPersonNationVo : OAPersonDefaultFieldVo
    {
        public OAPersonNationVo()
        {
            Name = "Nation";
            Text = "民族";
            Placeholder = "请填写民族";
            IsDefault = true;
            IsRequired = true;
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
