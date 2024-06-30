using OA.Domain.Enums;
using OneForAll.Core.Extension;
using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案合同信息设置-合同期限
    /// </summary>
    public class OAPersonContractLimitVo : OAPersonDefaultFieldVo
    {
        public OAPersonContractLimitVo()
        {
            Name = "ContractLimit";
            Text = "合同期限";
            Placeholder = "请选择合同期限";
            Type = OAPersonSettingFieldTypeEnum.Select;
            Value = "无";
            TypeDetail = new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = "无", Value = "无" },
                new OAPersonDefaultSelectVo() { Name = "3个月", Value = "3个月" },
                new OAPersonDefaultSelectVo() { Name = "6个月", Value = "6个月" },
                new OAPersonDefaultSelectVo() { Name = "12个月", Value = "12个月" },
                new OAPersonDefaultSelectVo() { Name = "24个月", Value = "24个月" },
                new OAPersonDefaultSelectVo() { Name = "36个月", Value = "36个月" },
                new OAPersonDefaultSelectVo() { Name = "36个月以上", Value = "36个月以上" }
            }.ToJson();
            IsDefault = true;
            IsRequired = true;
            IsShowEnabled = true;
            IsEnableText = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEmployeeEditable = true;
        }
    }
}
