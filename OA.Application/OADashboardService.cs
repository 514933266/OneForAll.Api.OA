using AutoMapper;
using Microsoft.AspNetCore.Http;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Enums;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Public.Models;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 仪表盘
    /// </summary>
    public class OADashboardService : OABaseService, IOADashboardService
    {
        private readonly IOAPersonManager _manager;
        private readonly IOATeamMemberManager _memberManager;

        private readonly IOAPersonRepository _repository;
        private readonly IOATeamRepository _teamRepository;
        private readonly IOATeamPersonContactRepository _contactRepository;
        public OADashboardService(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOAPersonManager manager,
            IOATeamMemberManager memberManager,
            IOAPersonRepository repository,
            IOATeamRepository teamRepository,
            IOATeamPersonContactRepository contactRepository) : base(mapper, httpContextAccessor)
        {
            _manager = manager;
            _memberManager = memberManager;
            _repository = repository;
            _teamRepository = teamRepository;
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// 获取新入职员工列表（近2个月）
        /// </summary>
        /// <returns>分页列表</returns>
        public async Task<IEnumerable<OAPersonBasicInfoDto>> GetListNewPersonAsync()
        {
            var data = await _repository.GetListNewEntryAsync();
            return _mapper.Map<IEnumerable<OAPerson>, IEnumerable<OAPersonBasicInfoDto>>(data);
        }

        /// <summary>
        /// 获取人员统计数据
        /// </summary>
        /// <returns></returns>
        public async Task<OAPersonStatisticV2Dto> GetStatisticsAsync()
        {
            var teamCount = 0;
            var groupCount = 0;
            var data = await _manager.GetListAsync(string.Empty, OAPersonOnJobStatusEnum.OnJob);
            var contacts = await _contactRepository.GetListByUserAsync(LoginUser.Id);
            if (contacts.Any())
            {
                var teamIds = new List<Guid>();
                var groupIds = new List<Guid>();
                var teams = await _teamRepository.GetListValidANTAsync();
                var tids = contacts.Where(w => w.IsLeader).Select(s => s.OATeamId).ToList();
                var gids = contacts.Where(w => !w.IsLeader).Select(s => s.OATeamId).ToList();
                tids.ForEach(e =>
                {
                    var children = teams.FindChildren(e);
                    teamIds.AddRange(children.Select(s => s.Id));
                });
                teamIds.AddRange(tids);
                gids.ForEach(e =>
                {
                    var children = teams.FindChildren(e);
                    groupIds.AddRange(children.Select(s => s.Id));
                });
                groupIds.AddRange(gids);
                if (teamIds.Any())
                    teamCount = await _contactRepository.GetCountByTeamAsync(teamIds);
                if (groupIds.Any())
                    groupCount = await _contactRepository.GetCountByTeamAsync(groupIds);
            }

            var result = new OAPersonStatisticV2Dto()
            {
                TotalOnJobCount = data.Count(),
                NormalCount = data.Where(w => w.EmployeeType == "全职").Count(),
                InternCount = data.Where(w => w.EmployeeType == "兼职").Count(),
                TrialCount = data.Where(w => w.EmployeeStatus == "实习生").Count(),
                TeamCount = teamCount,
                GroupCount = groupCount
            };
            return result;
        }
    }
}
