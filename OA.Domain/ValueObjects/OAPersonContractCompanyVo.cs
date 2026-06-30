using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案合同信息设置-合同公司
    /// </summary>
    public class OAPersonContractCompanyVo : OAPersonDefaultFieldVo
    {
        public OAPersonContractCompanyVo()
        {
            Name = "ContractCompany";
            Text = "合同公司";
            Placeholder = "请填写合同公司";
            IsDefault = true;
            IsRequired = true;
            IsEnableText = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEmployeeEditable = true;
        }
    }
}
