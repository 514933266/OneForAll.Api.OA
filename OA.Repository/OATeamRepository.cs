using OA.Domain;
using OA.Domain.AggregateRoots;
using OA.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core.ORM;
using OA.Domain.Enums;
using OneForAll.Core.Extension;
using OA.Domain.Aggregates;

namespace OA.Repository
{
    /// <summary>
    /// 团队组织
    /// </summary>
    public class OATeamRepository : Repository<OATeam>, IOATeamRepository
    {
        DbContext _context;
        public OATeamRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        #region 列表

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">上级id</param>
        /// <returns>团队列表</returns>
        public async Task<IEnumerable<OATeam>> GetListAsync(IEnumerable<Guid> ids)
        {
            var predicate = PredicateBuilder.Create<OATeam>(w => ids.Contains(w.Id));

            return await DbSet
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        /// <summary>
        /// 查询列表（未删除）
        /// </summary>
        /// <returns>团队列表</returns>
        public async Task<IEnumerable<OATeam>> GetListValidAsync()
        {
            return await DbSet
                .AsNoTracking()
                .Where(w => !w.IsDeleted)
                .ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="parentId">上级id</param>
        /// <param name="type">类型</param>
        /// <param name="scope">范围 -1全部 0有效 1被删除数据</param>
        /// <returns>团队列表</returns>
        public async Task<IEnumerable<OATeam>> GetListAsync(Guid parentId, string type, OATeamSearchScopeEnum scope)
        {
            var predicate = PredicateBuilder.Create<OATeam>(w => true);
            if (!type.IsNullOrEmpty()) predicate = predicate.And(w => w.Type == type);
            if (parentId != Guid.Empty) predicate = predicate.And(w => w.ParentId == parentId);

            switch (scope)
            {
                case OATeamSearchScopeEnum.Valid: predicate = predicate.And(w => !w.IsDeleted); break;
                case OATeamSearchScopeEnum.InValid: predicate = predicate.And(w => w.IsDeleted); break;
            }
            return await DbSet
                .AsNoTracking()
                .Where(predicate)
                .OrderBy(e => e.SortNumber)
                .ToListAsync();
        }

        #endregion

        #region 实体

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        public async Task<OATeam> GetAsync(Guid id)
        {
            return await DbSet.Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查询指定团队（含人员关联信息）
        /// </summary>
        /// <param name="id">组织架构id</param>
        /// <returns>实体</returns>
        public async Task<OATeam> GetWithContactAsync(Guid id)
        {
            return await DbSet
                .Where(w => w.Id.Equals(id))
                .FirstOrDefaultAsync();
        }
        #endregion

        #region 其他

        /// <summary>
        /// 是否有子级
        /// </summary>
        /// <param name="id">上级id</param>
        /// <returns>结果</returns>
        public async Task<bool> HasChildrenAsync(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .Where(w => w.ParentId == id && !w.IsDeleted)
                .CountAsync() > 0;
        }

        #endregion
    }
}
