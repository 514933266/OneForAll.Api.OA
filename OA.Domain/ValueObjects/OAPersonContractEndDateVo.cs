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
            Type = OAPersonSettingFieldTypeEnum.Date;
            IsDefault = true;
            IsRequired = true;
            IsShowEnabled = true;
            IsEnableText = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEmployeeEditable = true;
        }
    }
}
