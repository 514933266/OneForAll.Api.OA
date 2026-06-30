using Microsoft.EntityFrameworkCore;
using OA.Domain.AggregateRoots;
using OA.Domain.Repositorys;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    /// <summary>
    /// 人员档案字段设置
    /// </summary>
    public class OAPersonSettingFieldRepository : Repository<OAPersonSettingField>, IOAPersonSettingFieldRepository
    {
        public OAPersonSettingFieldRepository(DbContext context)
            : base(context)
        {

        }

        #region 列表

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OAPersonSettingField>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await DbSet
                .Where(w => ids.Contains(w.Id))
                .OrderBy(o => o.SortNumber)
                .ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OAPersonSettingField>> GetListBySettingAsync(Guid settingId)
        {
            return await DbSet
                .Where(w => w.OAPersonSettingId == settingId)
                .OrderBy(o => o.SortNumber)
                .ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="settingIds">设置id</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OAPersonSettingField>> GetListBySettingAsync(IEnumerable<Guid> settingIds)
        {
            return await DbSet
                .Where(w => settingIds.Contains(w.OAPersonSettingId))
                .OrderBy(o => o.SortNumber)
                .ToListAsync();
        }

        #endregion
    }
}
