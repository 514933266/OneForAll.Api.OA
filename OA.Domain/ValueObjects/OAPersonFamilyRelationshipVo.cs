using OA.Domain.Enums;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案家庭信息设置-关系(家人)
    /// </summary>
    public class OAPersonFamilyRelationshipVo : OAPersonDefaultFieldVo
    {
        public OAPersonFamilyRelationshipVo()
        {
            Name = "Relationship";
            Text = "关系(家人)";
            Placeholder = "请选择关系(家人)";
            Type = OAPersonSettingFieldTypeEnum.Select;
            TypeDetail = new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = "父母", Value = "父母" },
                new OAPersonDefaultSelectVo() { Name = "配偶", Value = "配偶" },
                new OAPersonDefaultSelectVo() { Name = "子女", Value = "子女" },
                new OAPersonDefaultSelectVo() { Name = "其他", Value = "其他" }
            }.ToJson();
            IsRequired = true;
            IsShowEnabled = true;
            IsEntryFileVisiable = true;
            IsEnableText = true;
            IsEnableType = true;
            IsEnableRequired = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
            IsEnableEmployeeEditable = true;
            IsEnableAddTypeDetail = true;
        }
    }
}
