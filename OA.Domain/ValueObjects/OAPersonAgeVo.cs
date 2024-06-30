using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-年龄
    /// </summary>
    public class OAPersonAgeVo : OAPersonDefaultFieldVo
    {
        public OAPersonAgeVo()
        {
            Name = "Age";
            Text = "年龄";
            Placeholder = "请填写年龄";
            Tips = "可根据[身份证]信息自动计算";
            IsDefault = true;
            IsShowEnabled = true;
            IsEnableRequired = true;
            IsEmployeeVisiable = true;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
