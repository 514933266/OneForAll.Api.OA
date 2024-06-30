using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案工作信息设置-试用期
    /// </summary>
    public class OAPersonTryDateVo : OAPersonDefaultFieldVo
    {
        public OAPersonTryDateVo()
        {
            Name = "TryDate";
            Text = "试用期";
            Placeholder = "请选择试用期";
            Type = Enums.OAPersonSettingFieldTypeEnum.Select;
            Value = "无";
            TypeDetail = new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = "无", Value = "无" },
                new OAPersonDefaultSelectVo() { Name = "1个月", Value = "1个月" },
                new OAPersonDefaultSelectVo() { Name = "3个月", Value = "3个月" },
                new OAPersonDefaultSelectVo() { Name = "6个月", Value = "6个月" },
                new OAPersonDefaultSelectVo() { Name = "其他", Value = "其他" }
            }.ToJson();
            IsDefault = true;
            IsRequired = true;
            IsShowEnabled = true;
            IsEnableType = true;
            IsEnableRequired = true;
            IsEnableEmployeeVisiable = true;
            IsEnableAddTypeDetail = true;
        }
    }
}
