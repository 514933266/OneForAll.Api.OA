using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案基础信息设置-司龄
    /// </summary>
    public class OAPersonJoinAgeVo : OAPersonDefaultFieldVo
    {
        public OAPersonJoinAgeVo()
        {
            Name = "JoinAge";
            Text = "司龄";
            Placeholder = "请填写司龄";
            Tips = "可根据[入职日期]信息自动计算，单位：年";
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = false;
            IsEnableEntryFileVisiable = false;
        }
    }
}
