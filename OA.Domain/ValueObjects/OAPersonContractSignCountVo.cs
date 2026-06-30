using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案合同信息设置-续签次数
    /// </summary>
    public class OAPersonContractSignCountVo : OAPersonDefaultFieldVo
    {
        public OAPersonContractSignCountVo()
        {
            Name = "ContractSignCount";
            Text = "续签次数";
            Placeholder = "请填写合同续签次数";
            IsRequired = true;
            IsShowEnabled = true;
            IsEnableText = true;
            IsEnableRequired = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEmployeeEditable = true;
        }
    }
}
