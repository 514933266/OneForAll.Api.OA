using OA.Domain.AggregateRoots;
using OA.Domain.Models;
using OneForAll.Core;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 钉钉接入设置
    /// </summary>
    public interface IOADingDingSettingManager
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns>列表</returns>
        Task<OADingDingSetting> GetAsync();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OADingDingSettingForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OADingDingSettingForm form);
    }
}
