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
    /// 人员档案个人信息设置-学历
    /// </summary>
    public class OAPersonEducationBackgroundVo : OAPersonDefaultFieldVo
    {
        public OAPersonEducationBackgroundVo()
        {
            Name = "EducationBackground";
            Text = "学历";
            Placeholder = "请选择学历";
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            Type = OAPersonSettingFieldTypeEnum.Select;
            TypeDetail = new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = "小学", Value = "小学" },
                new OAPersonDefaultSelectVo() { Name = "初中", Value = "初中" },
                new OAPersonDefaultSelectVo() { Name = "高中", Value = "高中" },
                new OAPersonDefaultSelectVo() { Name = "中专", Value = "中专" },
                new OAPersonDefaultSelectVo() { Name = "大专", Value = "大专" },
                new OAPersonDefaultSelectVo() { Name = "本科", Value = "本科" },
                new OAPersonDefaultSelectVo() { Name = "硕士", Value = "硕士" },
                new OAPersonDefaultSelectVo() { Name = "博士", Value = "博士" },
                new OAPersonDefaultSelectVo() { Name = "其他", Value = "其他" }
            }.ToJson();
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
