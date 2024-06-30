using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案基础信息设置-备注
    /// </summary>
    public class OAPersonRemarkVo : OAPersonDefaultFieldVo
    {
        public OAPersonRemarkVo()
        {
            Name = "Remark";
            Text = "备注";
            Placeholder = "请填写备注";
            IsShowEnabled = true;
            IsEnableText = true;
            IsEnableRequired = true;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
