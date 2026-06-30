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
    /// 人员档案个人信息设置-户籍类型
    /// </summary>
    public class OAPersonHouseholdTypeVo : OAPersonDefaultFieldVo
    {
        public OAPersonHouseholdTypeVo()
        {
            Name = "HouseholdType";
            Text = "户籍类型";
            Placeholder = "请选择户籍类型";
            Type = OAPersonSettingFieldTypeEnum.Select;
            Value = "无";
            TypeDetail = new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = "无", Value = "无" },
                new OAPersonDefaultSelectVo() { Name = "本地城镇", Value = "本地城镇" },
                new OAPersonDefaultSelectVo() { Name = "本地农村", Value = "本地农村" },
                new OAPersonDefaultSelectVo() { Name = "外地城镇（省内）", Value = "外地城镇（省内）" },
                new OAPersonDefaultSelectVo() { Name = "外地农村（省内）", Value = "外地农村（省内）" },
                new OAPersonDefaultSelectVo() { Name = "外地城镇（省外）", Value = "外地城镇（省外）" },
                new OAPersonDefaultSelectVo() { Name = "外地农村（省外）", Value = "外地农村（省外）" }
            }.ToJson();
            IsDefault = true;
            IsRequired = true;
            IsEnableType = true;
            IsShowEnabled = true;
            IsEnableRequired = true;
            IsEntryFileVisiable = true;
            IsEmployeeVisiable = true;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
            IsEnableAddTypeDetail = true;
        }
    }
}
