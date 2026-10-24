using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core.Extension;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Querys
{
    /// <summary>
    /// 部门类型查询服务
    /// </summary>
    public class OATeamTypeQueryService : IOATeamTypeQueryService
    {
        private readonly IOATeamTypeRepository _repository;

        public OATeamTypeQueryService(
            IOATeamTypeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamType>> GetListAsync(string name)
        {
            if (!name.IsNullOrEmpty())
            {
                return await _repository.GetListAsync(name);
            }
            else
            {
                return await _repository.GetListAsync();
            }
        }
    }
}
