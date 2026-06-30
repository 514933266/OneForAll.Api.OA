using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;
using OneForAll.Core.Extension;

namespace OA.Repository
{
    /// <summary>
    /// 部门类型
    /// </summary>
    public class OATeamTypeRepository : Repository<OATeamType>, IOATeamTypeRepository
    {
        public OATeamTypeRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamType>> GetListAsync(string name)
        {
            return await DbSet
                .AsNoTracking()
                .Where(w => w.Name.Contains(name))
                .ToListAsync();
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>列表</returns>
        public async Task<OATeamType> GetAsync(string name)
        {
            return await DbSet
                .Where(w => w.Name.Contains(name))
                .FirstOrDefaultAsync();
        }
    }
}
