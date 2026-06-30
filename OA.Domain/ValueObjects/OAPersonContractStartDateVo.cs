using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案合同信息设置-合同起始日
    /// </summary>
    public class OAPersonContractStartDateVo : OAPersonDefaultFieldVo
    {
        public OAPersonContractStartDateVo()
        {
            Name = "ContractFirstDate";
            Text = "合同起始日";
            Placeholder = "请选择合同起始日";
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
