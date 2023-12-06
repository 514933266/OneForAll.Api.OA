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
            IsRequired = true;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            IsEntryFileVisiable = true;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
