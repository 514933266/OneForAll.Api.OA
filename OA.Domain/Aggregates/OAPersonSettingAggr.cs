using OA.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Aggregates
{
    /// <summary>
    /// 字段设置
    /// </summary>
    public class OAPersonSettingAggr : OAPersonSetting
    {
        /// <summary>
        /// 明细
        /// </summary>
        public List<OAPersonSettingField> OAPersonSettingFields { get; set; }
    }
}
