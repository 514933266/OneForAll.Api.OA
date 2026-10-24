using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core.Extension;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.Aggregates;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Querys
{
    /// <summary>
    /// 部门成员查询服务
    /// </summary>
    public class OATeamMemberQueryService : IOATeamMemberQueryService
    {
        private readonly IOATeamRepository _teamRepository;
        private readonly IOAPersonRepository _personReposiotry;
        private readonly IOATeamPersonContactRepository _repository;

        public OATeamMemberQueryService(
            IOATeamRepository teamRepository,
            IOAPersonRepository personReposiotry,
            IOATeamPersonContactRepository repository)
        {
            _repository = repository;
            _teamRepository = teamRepository;
            _personReposiotry = personReposiotry;
        }

        /// <summary>
        /// 查询在职列表（含人员基础信息）
        /// </summary>
        /// <param name="teamId">组织id</param>
        /// <param name="deep">是否递归</param>
        /// <returns>成员列表</returns>
        public async Task<IEnumerable<OATeamMemberAggr>> GetListAsync(Guid teamId, bool deep)
        {
            if (deep)
            {
                var teams = await _teamRepository.GetListAsync();
                var childTeamIds = teams.FindChildren(teamId).Select(s => s.Id);

                var data = await _repository.GetListByTeamAsync(childTeamIds);
                return data.DistinctBy(d => d.Id);
            }
            else
            {
                var data = await _repository.GetListByTeamAsync(teamId);
                return data.DistinctBy(d => d.Id);
            }
        }

        /// <summary>
        /// 查询未加入团队列表
        /// </summary>
        /// <returns>成员列表</returns>
        public async Task<IEnumerable<OAPerson>> GetListNoTeamAsync()
        {
            return await _personReposiotry.GetListNoTeamAsync();
        }

        /// <summary>
        /// 查询指定人员的团队关联记录（含团队信息）
        /// </summary>
        /// <param name="personId">人员id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamMemberAggr>> GetListByPersonAsync(Guid personId)
        {
            return await _repository.GetListByPersonAsync(personId);
        }

        /// <summary>
        /// 查询当前用户所在的团队关联记录
        /// </summary>
        /// <param name="loginUserId">当前登录用户id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAMidTeamPerson>> GetListByUserAsync(Guid loginUserId)
        {
            return await _repository.GetListByUserAsync(loginUserId);
        }

        /// <summary>
        /// 查询团队成员数量
        /// </summary>
        /// <param name="teamIds">团队id</param>
        /// <returns>数量</returns>
        public async Task<int> GetCountByTeamAsync(IEnumerable<Guid> teamIds)
        {
            return await _repository.GetCountByTeamAsync(teamIds);
        }
    }
}
