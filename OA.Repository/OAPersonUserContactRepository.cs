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
    /// 人员档案
    /// </summary>
    public class OAPersonUserContactRepository : Repository<OAPersonUserContact>, IOAPersonUserContactRepository
    {
        public OAPersonUserContactRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// 查询人员档案
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>人员</returns>
        public async Task<OAPerson> GetByUserIdAsync(Guid userId)
        {
            var personDbSet = Context.Set<OAPerson>();
            var sql = (from data in DbSet
                       join person in personDbSet on data.OAPersonId equals person.Id
                       where data.SysUserId == userId
                       select person);

            return await sql.FirstOrDefaultAsync();
        }
    }
}
