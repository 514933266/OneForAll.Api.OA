using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案基础信息设置-工号
    /// </summary>
    public class OAPersonWorkNumberVo : OAPersonDefaultFieldVo
    {
        public OAPersonWorkNumberVo()
        {
            Name = "WorkNumber";
            Text = "工号";
            Placeholder = "请填写员工工号";
            IsDefault = true;
            IsShowEnabled = true;
            IsEnableRequired = true;
            IsEmployeeVisiable = true;
            IsEnableEmployeeVisiable = true;
        }
    }
}
