﻿using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-毕业时间
    /// </summary>
    public class OAPersonSchoolDateVo : OAPersonDefaultFieldVo
    {
        public OAPersonSchoolDateVo()
        {
            Name = "SchoolDate";
            Text = "毕业时间";
            Placeholder = "请选择毕业时间";
            IsDefault = false;
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            Type = OAPersonSettingFieldTypeEnum.Date;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
