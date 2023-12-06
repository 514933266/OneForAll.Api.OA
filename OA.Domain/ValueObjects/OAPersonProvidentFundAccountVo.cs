using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-公积金账号
    /// </summary>
    public class OAPersonProvidentFundAccountVo : OAPersonDefaultFieldVo
    {
        public OAPersonProvidentFundAccountVo()
        {
            Name = "ProvidentFundAccount";
            Text = "公积金账号";
            Placeholder = "请填写公积金账号";
            IsDefault = false;
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeVisiable = false;
            IsEmployeeEditable = false;
            IsEntryFileVisiable = false;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
