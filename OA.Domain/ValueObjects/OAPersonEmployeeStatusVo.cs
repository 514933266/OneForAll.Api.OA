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
    /// 人员档案工作信息设置-员工状态
    /// </summary>
    public class OAPersonEmployeeStatusVo : OAPersonDefaultFieldVo
    {
        public OAPersonEmployeeStatusVo()
        {
            Name = "Status";
            Text = "员工状态";
            Placeholder = "请选择员工状态";
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            Type = OAPersonSettingFieldTypeEnum.Select;
            Value = "正式员工";
            TypeDetail = GetListSelection().ToJson();
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = false;
            IsEnableEntryFileVisiable = false;
        }

        /// <summary>
        /// 获取下拉选项列表
        /// </summary>
        /// <returns>下拉选项列表</returns>
        public static List<OAPersonDefaultSelectVo> GetListSelection()
        {
            return new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = "正式员工", Value = "正式员工" },
                new OAPersonDefaultSelectVo() { Name = "试用员工", Value = "试用员工" }
            };
        }

        /// <summary>
        /// 获取默认选项
        /// </summary>
        /// <returns>选项</returns>
        public static OAPersonDefaultSelectVo GetDefaultSelection()
        {
            return GetListSelection().FirstOrDefault(w => w.Name == "正式员工");
        }
    }
}
