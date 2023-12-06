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
    /// 人员档案个人信息设置-婚姻状况
    /// </summary>
    public class OAPersonMaritalStatusVo : OAPersonDefaultFieldVo
    {
        public OAPersonMaritalStatusVo()
        {
            Name = "MaritalStatus";
            Text = "婚姻状况";
            Placeholder = "请选择婚姻状况";
            IsRequired = true;
            IsEnableRequired = true;
            Type = OAPersonSettingFieldTypeEnum.Select;
            Value = "未婚";
            TypeDetail = new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = "未婚", Value = "未婚" },
                new OAPersonDefaultSelectVo() { Name = "已婚", Value = "已婚" },
                new OAPersonDefaultSelectVo() { Name = "离异", Value = "离异" },
                new OAPersonDefaultSelectVo() { Name = "丧偶", Value = "丧偶" },
                new OAPersonDefaultSelectVo() { Name = "分居", Value = "分居" }
            }.ToJson();
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
