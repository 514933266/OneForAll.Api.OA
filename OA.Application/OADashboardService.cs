using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Enums;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OneForAll.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 仪表盘
    /// </summary>
    public class OADashboardService : IOADashboardService
    {
        private readonly IMapper _mapper;
        private readonly IOATeamMemberManager _memberManager;
        private readonly IOAPersonQueryService _personQueryService;

        public OADashboardService(
            IMapper mapper,
            IOATeamMemberManager memberManager,
            IOAPersonQueryService personQueryService)
        {
            _mapper = mapper;
            _memberManager = memberManager;
            _personQueryService = personQueryService;
        }

        /// <summary>
        /// 获取新入职员工列表（近2个月）
        /// </summary>
        /// <returns>分页列表</returns>
        public async Task<IEnumerable<OAPersonBasicInfoDto>> GetListNewPersonAsync()
        {
            var data = await _personQueryService.GetListNewEntryAsync();
            if (data.Any())
            {
                return _mapper.Map<IEnumerable<OAPerson>, IEnumerable<OAPersonBasicInfoDto>>(data);
            }
            else
            {
                return new List<OAPersonBasicInfoDto>();
            }
        }

        /// <summary>
        /// 获取人员统计数据
        /// </summary>
        /// <returns></returns>
        public async Task<OAPersonStatisticV2Dto> GetStatisticsAsync()
        {
            // 人员统计口径与管辖部门/群组人数均由领域层给出
            var persons = await _personQueryService.GetListAsync(string.Empty, OAPersonOnJobStatusEnum.OnJob);
            var statistic = OAPersonStatistic.Count(persons);
            var led = await _memberManager.GetLedCountAsync();
            statistic.TeamCount = led.TeamCount;
            statistic.GroupCount = led.GroupCount;
            return _mapper.Map<OAPersonStatistic, OAPersonStatisticV2Dto>(statistic);
        }
    }
}
