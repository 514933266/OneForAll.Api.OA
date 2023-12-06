using Microsoft.EntityFrameworkCore;
using OA.Domain.AggregateRoots;
using OA.Domain.Enums;
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
    ///  表格自定义字段显示配置
    /// </summary>
    public class OATableColumnSettingsRepository : Repository<OATableColumnSetting>, IOATableColumnSettingsRepository
    {
        public OATableColumnSettingsRepository(DbContext context)
            : base(context)
        {
        }


        /// <summary>
        /// 查询指定用户配置
        /// </summary>
        /// <param name="target">类型</param>
        /// <param name="creatorId">创建人id</param>
        /// <returns></returns>
        public async Task<OATableColumnSetting> GetAsync(OATableColumnSettingsTargetEnum target, Guid creatorId)
        {
            return await DbSet.Where(w => w.Target == target && w.CreatorId == creatorId).FirstOrDefaultAsync();
        }
    }
}
