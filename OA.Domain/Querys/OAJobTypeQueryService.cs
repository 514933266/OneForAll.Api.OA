using OneForAll.Core.Extension;
using System.Threading.Tasks;
using OA.Domain.AggregateRoots;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using System.Collections.Generic;

namespace OA.Domain.Querys
{
    /// <summary>
    /// 职级类型查询服务
    /// </summary>
    public class OAJobTypeQueryService : IOAJobTypeQueryService
    {
        private readonly IOAJobTypeRepository _repository;

        public OAJobTypeQueryService(
            IOAJobTypeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAJobType>> GetListAsync(string key)
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
