using Microsoft.EntityFrameworkCore;
using OA.Domain.AggregateRoots;
using OA.Domain.Enums;
using OA.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    /// <summary>
    /// 人员档案配置
    /// </summary>
    public class OAPersonSettingRepository : Repository<OAPersonSetting>, IOAPersonSettingRepository
    {
        public OAPersonSettingRepository(DbContext context)
            : base(context)
        {

        }

        #region 列表

        /// <summary>
        /// 查询列表（跟踪）
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OAPersonSetting>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await DbSet.Where(w => ids.Contains(w.Id)).ToListAsync();
        }

        /// <summary>
        /// 查询指定租户的配置列表
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonSetting>> GetListByTenantIQFAsync(Guid tenantId)
        {
            return await DbSet.IgnoreQueryFilters().Where(w => w.SysTenantId == tenantId).ToListAsync();
        }

        #endregion

        #region 其他

        /// <summary>
        /// 查询数据量
        /// </summary>
        /// <returns>总数据量</returns>
        public async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }

        #endregion
    }
}
