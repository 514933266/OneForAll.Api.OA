using OneForAll.Core;
using OneForAll.Core.Extension;
using System.Threading.Tasks;
using OA.Domain.AggregateRoots;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using System.Collections.Generic;

namespace OA.Domain.Querys
{
    /// <summary>
    /// 职务查询服务
    /// </summary>
    public class OAJobDutyQueryService : IOAJobDutyQueryService
    {
        private readonly IOAJobDutyRepository _repository;

        public OAJobDutyQueryService(
            IOAJobDutyRepository repository)
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
        public async Task<PageList<OAJobDuty>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            return await _repository.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAJobDuty>> GetListAsync(string key)
        {
            if (key.IsNullOrEmpty())
            {
                return await _repository.GetListAsync();
            }
            else
            {
                return await _repository.GetListByNameContainsAsync(key);
            }
        }
    }
}
