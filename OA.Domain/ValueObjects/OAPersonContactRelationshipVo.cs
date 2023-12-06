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
    /// 人员档案紧急联系人设置-联系人关系
    /// </summary>
    public class OAPersonContactRelationshipVo : OAPersonDefaultFieldVo
    {
        public OAPersonContactRelationshipVo()
        {
            Name = "ContactRelationship";
            Text = "联系人关系";
            Placeholder = "请选择联系人关系";
            IsDefault = false;
            IsRequired = true;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            Type = OAPersonSettingFieldTypeEnum.Select;
            Value = "无";
            TypeDetail = new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = "无", Value = "无" },
                new OAPersonDefaultSelectVo() { Name = "父母", Value = "父母" },
                new OAPersonDefaultSelectVo() { Name = "配偶", Value = "配偶" },
                new OAPersonDefaultSelectVo() { Name = "子女", Value = "子女" },
                new OAPersonDefaultSelectVo() { Name = "其他", Value = "其他" }
            }.ToJson();
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
