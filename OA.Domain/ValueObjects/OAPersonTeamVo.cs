using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案基础信息设置-部门
    /// </summary>
    public class OAPersonTeamVo : OAPersonDefaultFieldVo
    {
        public OAPersonTeamVo()
        {
            Name = "Team";
            Text = "部门";
            Placeholder = "请填写部门名称";
            Tips = "可根据[组织架构关系]自动生成";
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
