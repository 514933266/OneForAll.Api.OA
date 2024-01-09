using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
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

namespace OA.Domain
{
    /// <summary>
    /// 企业组织架构
    /// </summary>
    public class OATeamManager : OABaseManager, IOATeamManager
    {
        private readonly IOATeamRepository _repository;
        private readonly IOATeamTypeRepository _typeRepository;
        private readonly IOAPersonRepository _personRepository;
        private readonly IOATeamPersonContactRepository _contactRepository;

        public OATeamManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOATeamRepository repository,
            IOATeamTypeRepository typeRepository,
            IOAPersonRepository personRepository,
            IOATeamPersonContactRepository contactRepository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
            _typeRepository = typeRepository;
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
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OATeamForm entity)
        {
            var teams = await _repository.GetListValidANTAsync();
            var data = _mapper.Map<OATeamForm, OATeam>(entity);
            data.CreateTime = DateTime.Now;
            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;
            data.SysTenantId = LoginUser.SysTenantId;

            var parent = new OATeam() { Id = data.ParentId };
            if (data.ParentId != Guid.Empty)
            {
                parent = teams.FirstOrDefault(w => w.Id == data.ParentId);
                if (parent == null) return BaseErrType.DataNotFound;

                var type = await _typeRepository.GetAsync(parent.Type);
                if (type != null && !type.CanAddChild) return BaseErrType.NotAllow;
            }

            // 同级不允许重名
            var siblings = teams.Where(w => w.ParentId == parent.Id).ToList();
            if (siblings.Any(w => w.Name == data.Name)) return BaseErrType.DataExist;

            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OATeamForm entity)
        {
            var teams = await _repository.GetListAsync();
            var data = teams.FirstOrDefault(w => w.Id == entity.Id);
            if (data == null) return BaseErrType.DataError;

            _mapper.Map(entity, data);

            var parent = new OATeam() { Id = data.ParentId };
            if (data.ParentId != Guid.Empty)
            {
                parent = teams.FirstOrDefault(w => w.Id == data.ParentId);
                if (parent == null) return BaseErrType.DataNotFound;

                var type = await _typeRepository.GetAsync(parent.Type);
                if (type != null && !type.CanAddChild) return BaseErrType.NotAllow;
            }

            // 同级不允许重名
            var siblings = teams.Where(w => w.ParentId == parent.Id).ToList();
            if (siblings.Any(w => w.Name == data.Name && w.Id != data.Id)) return BaseErrType.DataExist;

            return await ResultAsync(_repository.SaveChangesAsync);
        }

        /// <summary>
        /// 移除/恢复
        /// </summary>
        /// <param name="id">组织架构id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid id)
        {
            var data = await _repository.GetWithContactAsync(id);
            if (data == null) return BaseErrType.DataNotFound;

            var hasChildren = await _repository.HasChildrenAsync(id);
            if (hasChildren) return BaseErrType.NotAllow;

            data.IsDeleted = !data.IsDeleted;
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="id">组织架构id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> TrueDeleteAsync(Guid id)
        {
            var data = await _repository.GetWithContactAsync(id);
            if (data == null) return BaseErrType.DataNotFound;
            if (!data.IsDeleted) return BaseErrType.DataError;

            return await ResultAsync(() => _repository.DeleteAsync(data));
        }

        /// <summary>
        /// 批量排序
        /// </summary>
        /// <param name="entities">排序表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> SortAsync(IEnumerable<OATeamSortForm> entities)
        {
            var ids = entities.Select(s => s.Id);
            var data = await _repository.GetListANTAsync(ids);
            if (data == null) return BaseErrType.DataNotFound;

            var index = 0;
            entities.ForEach(e =>
            {
                var item = data.FirstOrDefault(w => w.Id == e.Id);
                if (item != null)
                {
                    item.SortNumber = index;
                    index++;
                }
            });
            return await ResultAsync(() => _repository.UpdateRangeAsync(data));
        }
    }
}
