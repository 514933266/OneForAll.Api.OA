﻿using OA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案基础信息设置-职位
    /// </summary>
    public class OAPersonJobVo : OAPersonDefaultFieldVo
    {
        public OAPersonJobVo()
        {
            Name = "Job";
            Text = "职位";
            Placeholder = "请填写职位";
            IsDefault = true;
            IsRequired = true;
            IsEnableText = true;
            IsEmployeeVisiable = true;
            IsEnableRequired = true;
            IsEmployeeVisiable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
