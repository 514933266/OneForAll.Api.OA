﻿using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案家庭信息设置-性别(家人)
    /// </summary>
    public class OAPersonFamilySexVo : OAPersonDefaultFieldVo
    {
        public OAPersonFamilySexVo()
        {
            Name = "FamilySex";
            Text = "性别(家人)";
            Placeholder = "请填写性别(家人)";
            IsDefault = false;
            IsRequired = true;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            Type = Enums.OAPersonSettingFieldTypeEnum.Radio;
            Value = "1";
            TypeDetail = new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = "女", Value = "女" },
                new OAPersonDefaultSelectVo() { Name = "男", Value = "男" },
            }.ToJson();
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
