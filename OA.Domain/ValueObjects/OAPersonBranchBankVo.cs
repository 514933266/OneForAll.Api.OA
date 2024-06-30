using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案银行卡信息设置-开户行支行
    /// </summary>
    public class OAPersonBranchBankVo : OAPersonDefaultFieldVo
    {
        public OAPersonBranchBankVo()
        {
            Name = "BranchBank";
            Text = "开户行支行";
            Placeholder = "请填写开户行支行";
            IsShowEnabled = true;
            IsEnableRequired = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
