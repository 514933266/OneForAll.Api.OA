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
    /// 人员档案合同信息设置-合同类型
    /// </summary>
    public class OAPersonContractTypeVo : OAPersonDefaultFieldVo
    {
        public OAPersonContractTypeVo()
        {
            Name = "ContractType";
            Text = "合同类型";
            Placeholder = "请选择合同类型";
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            IsEnableAddTypeDetail = true;
            Type = OAPersonSettingFieldTypeEnum.Select;
            Value = "固定期限劳动合同";
            TypeDetail = new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = "固定期限劳动合同", Value = "固定期限劳动合同" },
                new OAPersonDefaultSelectVo() { Name = "无固定期限劳动合同", Value = "无固定期限劳动合同" },
                new OAPersonDefaultSelectVo() { Name = "短期劳动合同", Value = "短期劳动合同" },
                new OAPersonDefaultSelectVo() { Name = "劳务派遣合同", Value = "劳务派遣合同" },
                new OAPersonDefaultSelectVo() { Name = "实习协议", Value = "实习协议" },
                new OAPersonDefaultSelectVo() { Name = "劳务协议", Value = "劳务协议" },
                new OAPersonDefaultSelectVo() { Name = "返聘协议", Value = "返聘协议" },
                new OAPersonDefaultSelectVo() { Name = "其他", Value = "其他" }
            }.ToJson();
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = false;
            IsEnableEntryFileVisiable = false;
        }
    }
}
