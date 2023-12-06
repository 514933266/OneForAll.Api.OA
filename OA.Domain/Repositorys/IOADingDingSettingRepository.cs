using OA.Domain.AggregateRoots;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Repositorys
{
    /// <summary>
    /// 钉钉接入设置
    /// </summary>
    public interface IOADingDingSettingRepository : IEFCoreRepository<OADingDingSetting>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <returns>列表</returns>
        Task<OADingDingSetting> GetAsync();

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <returns>列表</returns>
        Task<int> CountAsync();
    }
}
