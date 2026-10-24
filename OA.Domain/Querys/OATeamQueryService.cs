using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OA.Domain.Enums;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Aggregates;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;
using OA.Domain.ValueObjects;

namespace OA.Domain.Querys
{
    /// <summary>
    /// 企业组织架构查询服务
    /// </summary>
    public class OATeamQueryService : IOATeamQueryService
    {
        private readonly IMapper _mapper;
        private readonly IOATeamRepository _repository;
        private readonly IOAPersonRepository _personRepository;
        private readonly IOATeamPersonContactRepository _contactRepository;

        public OATeamQueryService(
            IMapper mapper,
            IOATeamRepository repository,
            IOAPersonRepository personRepository,
            IOATeamPersonContactRepository contactRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _personRepository = personRepository;
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// 获取列表树
        /// </summary>
        /// <param name="parentId">上级id</param>
        /// <param name="type">类型</param>
        /// <param name="deep">是否深度检索</param>
        /// <param name="scope">范围 -1全部 0有效 1被删除数据</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamTreeAggr>> GetListAsync(Guid parentId, string type, bool deep, OATeamSearchScopeEnum scope)
        {
            var id = deep ? Guid.Empty : parentId;
            var data = await _repository.GetListANTAsync(Guid.Empty, type, scope) as List<OATeam>;

            var leaderIds = data.Select(s => s.LeaderId).ToList();
            var leaders = await _personRepository.GetListAsync(leaderIds);

            // 直属主管信息
            var result = _mapper.Map<IEnumerable<OATeam>, IEnumerable<OATeamTreeAggr>>(data);
            var rootNodeList = new List<OATeamTreeAggr>();
            var ids = result.Select(s => s.Id).ToList();
            result.ForEach(e =>
            {
                var leader = leaders.FirstOrDefault(w => w.Id == e.LeaderId);
                if (leader != null)
                {
                    e.LeaderName = leader.Name;
                }
                else if (e.LeaderId != Guid.Empty)
                {
                    e.LeaderName = "档案丢失";
                }
                if (e.ParentId != Guid.Empty && !ids.Contains(e.ParentId))
                    rootNodeList.Add(e);
            });

            var tree = result.ToTree<OATeamTreeAggr, Guid>().FindChildren(id, deep).ToList();
            tree.AddRange(rootNodeList);

            var contacts = await _contactRepository.GetListCountByTeamAsync(ids);
            CalculateMemberCount(tree, contacts);
            return tree;
        }

        // 计算成员数量，上级 = 自身成员数量 + 子级数量
        private void CalculateMemberCount(IEnumerable<OATeamTreeAggr> teams, IEnumerable<OATeamMemberCountVo> sources)
        {
            teams.ForEach(team =>
            {
                var count = sources.FirstOrDefault(w => w.TeamId == team.Id);
                team.MemberNumber = count == null ? 0 : count.Count;
                if (team.Children.Any())
                {
                    CalculateMemberCount(team.Children, sources);
                    team.Children.ForEach(child =>
                    {
                        team.MemberNumber += child.MemberNumber;
                    });
                }
            });
        }

        /// <summary>
        /// 获取指定组织
        /// </summary>
        /// <param name="id">组织id</param>
        /// <returns>列表</returns>
        public async Task<OATeam> GetAsync(Guid id)
        {
            return await _repository.FindAsync(id);
        }

        /// <summary>
        /// 获取有效组织列表
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeam>> GetListValidAsync()
        {
            return await _repository.GetListValidANTAsync();
        }

        /// <summary>
        /// 按组织id集合获取组织列表
        /// </summary>
        /// <param name="ids">组织id集合</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeam>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await _repository.GetListANTAsync(ids);
        }
    }
}
