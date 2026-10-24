using OneForAll.Core;
using System.Threading.Tasks;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using System.Collections.Generic;

namespace OA.Domain.Querys
{
    /// <summary>
    /// 职位查询服务
    /// </summary>
    public class OAJobQueryService : IOAJobQueryService
    {
        private readonly IOAJobRepository _repository;

        public OAJobQueryService(
            IOAJobRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<OAJob>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            return await _repository.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="isEnabled">是否启用</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAJobAggr>> GetListAsync(bool? isEnabled, string key)
        {
            return await _repository.GetListAsync(isEnabled, key);
        }
    }
}
