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
            IsDefault = false;
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
