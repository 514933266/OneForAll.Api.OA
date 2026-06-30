using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案银行卡信息设置-开户行
    /// </summary>
    public class OAPersonBankVo : OAPersonDefaultFieldVo
    {
        public OAPersonBankVo()
        {
            Name = "Bank";
            Text = "开户行";
            Placeholder = "请填写开户行";
            IsDefault = true;
            IsEnableRequired = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
