using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.EFCore;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Repositorys
{
    /// <summary>
    /// 人员档案配置
    /// </summary>
    public interface IOAPersonSettingRepository : IEFCoreRepository<OAPersonSetting>
    {
        #region 列表

        /// <summary>
        /// 查询列表（跟踪）
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OAPersonSetting>> GetListAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 查询指定租户的配置列表
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPersonSetting>> GetListByTenantIQFAsync(Guid tenantId);

        #endregion

        #region 其他

        /// <summary>
        /// 查询数据量
        /// </summary>
        /// <returns>总数据量</returns>
        Task<int> CountAsync();

        #endregion
    }
}
