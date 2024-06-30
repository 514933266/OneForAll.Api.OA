using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-工龄
    /// </summary>
    public class OAPersonWorkAgeVo : OAPersonDefaultFieldVo
    {
        public OAPersonWorkAgeVo()
        {
            Name = "WorkAge";
            Text = "工龄";
            Placeholder = "请填写工龄";
            Tips = "可根据[首次参加工作时间]信息自动计算，单位：年";
            IsDefault = true;
            IsShowEnabled = true;
            IsEnableRequired = true;
            IsEmployeeVisiable = true;
            IsEnableEmployeeVisiable = true;
        }
    }
}
