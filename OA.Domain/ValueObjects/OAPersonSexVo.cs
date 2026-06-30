using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-性别
    /// </summary>
    public class OAPersonSexVo : OAPersonDefaultFieldVo
    {
        public OAPersonSexVo()
        {
            Name = "Sex";
            Text = "性别";
            Placeholder = "请填写性别";
            Tips = "可根据[身份证]信息自动计算";
            Type = Enums.OAPersonSettingFieldTypeEnum.Radio;
            Value = "男";
            TypeDetail = new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = "女", Value = "女" },
                new OAPersonDefaultSelectVo() { Name = "男", Value = "男" },
            }.ToJson();
            IsDefault = true;
            IsShowEnabled = true;
            IsEnableRequired = true;
            IsEmployeeVisiable = true;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
