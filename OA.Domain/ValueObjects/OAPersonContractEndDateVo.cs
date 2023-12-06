using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案合同信息设置-合同结束日
    /// </summary>
    public class OAPersonContractEndDateVo : OAPersonDefaultFieldVo
    {
        public OAPersonContractEndDateVo()
        {
            Name = "ContractEndDate";
            Text = "合同结束日";
            Placeholder = "请选择合同结束日";
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            Type = OAPersonSettingFieldTypeEnum.Date;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = false;
            IsEnableEntryFileVisiable = false;
        }
    }
}
