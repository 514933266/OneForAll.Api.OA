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
        /// <summary>
        /// 正式员工
        /// </summary>
        public const string ValueNormal = "正式员工";

        /// <summary>
        /// 试用员工
        /// </summary>
        public const string ValueTrial = "试用员工";

        public OAPersonEmployeeStatusVo()
        {
            Name = "Status";
            Text = "员工状态";
            Placeholder = "请选择员工状态";
            Type = OAPersonSettingFieldTypeEnum.Select;
            Value = ValueNormal;
            TypeDetail = GetListSelection().ToJson();
            IsDefault = true;
            IsShowEnabled = true;
            IsEnableText = true;
            IsEnableRequired = true;
            IsEnableEmployeeVisiable = true;
            IsEnableAddTypeDetail = true;
            IsEnableEntryFileVisiable = true;
        }

        /// <summary>
        /// 获取下拉选项列表
        /// </summary>
        /// <returns>下拉选项列表</returns>
        public static List<OAPersonDefaultSelectVo> GetListSelection()
        {
            return new List<OAPersonDefaultSelectVo>()
            {
                new OAPersonDefaultSelectVo() { Name = ValueNormal, Value = ValueNormal },
                new OAPersonDefaultSelectVo() { Name = ValueTrial, Value = ValueTrial }
            };
        }

        /// <summary>
        /// 获取默认选项
        /// </summary>
        /// <returns>选项</returns>
        public static OAPersonDefaultSelectVo GetDefaultSelection()
        {
            return GetListSelection().FirstOrDefault(w => w.Name == ValueNormal);
        }
    }
}
