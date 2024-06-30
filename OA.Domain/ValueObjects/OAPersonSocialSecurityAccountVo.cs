using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-社保账号
    /// </summary>
    public class OAPersonSocialSecurityAccountVo : OAPersonDefaultFieldVo
    {
        public OAPersonSocialSecurityAccountVo()
        {
            Name = "SocialSecurityAccount";
            Text = "社保账号";
            Placeholder = "请填写社保账号";
            IsShowEnabled = true;
            IsEnableRequired = true;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
