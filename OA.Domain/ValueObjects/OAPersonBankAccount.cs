using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案银行卡信息设置-银行卡号
    /// </summary>
    public class OAPersonBankAccount : OAPersonDefaultFieldVo
    {
        public OAPersonBankAccount()
        {
            Name = "BankAccount";
            Text = "银行卡号";
            Placeholder = "请填写银行卡号";
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
