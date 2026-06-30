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
    /// 钉钉接入设置
    /// </summary>
    public class OADingDingSettingRepository : Repository<OADingDingSetting>, IOADingDingSettingRepository
    {
        public OADingDingSettingRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <returns>列表</returns>
        public async Task<OADingDingSetting> GetAsync()
        {
            return await DbSet.FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <returns>列表</returns>
        public async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }
    }
}
