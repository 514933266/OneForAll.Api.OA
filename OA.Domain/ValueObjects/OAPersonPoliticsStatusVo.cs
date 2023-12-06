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
    /// 人员档案个人信息设置-政治面貌
    /// </summary>
    public class OAPersonPoliticsStatusVo : OAPersonDefaultFieldVo
    {
        public OAPersonPoliticsStatusVo()
        {
            Name = "PoliticsStatus";
            Text = "政治面貌";
            Placeholder = "请选择政治面貌";
            IsDefault = false;
            IsRequired = true;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            Type = OAPersonSettingFieldTypeEnum.Select;
            Value = "群众";
            TypeDetail = new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = "团员", Value = "团员" },
                new OAPersonDefaultSelectVo() { Name = "党员", Value = "党员" },
                new OAPersonDefaultSelectVo() { Name = "群众", Value = "群众" },
                new OAPersonDefaultSelectVo() { Name = "其他", Value = "其他" }
            }.ToJson();
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
