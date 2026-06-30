using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案基础信息设置-直属主管
    /// </summary>
    public class OAPersonTeamLeaderVo : OAPersonDefaultFieldVo
    {
        public OAPersonTeamLeaderVo()
        {
            Name = "TeamLeaderName";
            Text = "直属主管";
            Placeholder = "请填写直属主管姓名";
            Tips = "可根据[组织架构关系]自动生成";
            IsDefault = true;
            IsShowEnabled = true;
            IsEmployeeVisiable = true;
        }
    }
}
