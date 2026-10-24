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
    /// 职级管理
    /// </summary>
    public class OAJobLevelRepository : Repository<OAJobLevel>, IOAJobLevelRepository
    {
        public OAJobLevelRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<OAJobLevel>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<OAJobLevel>(w => true);
            if (!key.IsNullOrEmpty())
            {
                predicate = predicate.And(w => w.Name.Contains(key));
            }

            var total = await DbSet.CountAsync(predicate);

            var data = await DbSet
                .Where(predicate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageList<OAJobLevel>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 按名称查询
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>实体</returns>
        public async Task<OAJobLevel> GetByNameAsync(string name)
        {
            return await DbSet.FirstOrDefaultAsync(w => w.Name == name);
        }

        /// <summary>
        /// 按名称模糊查询
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        public async Task<List<OAJobLevel>> GetListByNameContainsAsync(string key)
        {
            return await DbSet.Where(w => w.Name.Contains(key)).ToListAsync();
        }

        /// <summary>
        /// 按id集合查询
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns>列表</returns>
        public async Task<List<OAJobLevel>> GetListByIdsAsync(IEnumerable<Guid> ids)
        {
            return await DbSet.Where(w => ids.Contains(w.Id)).ToListAsync();
        }
    }
}
