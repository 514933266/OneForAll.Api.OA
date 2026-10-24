using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Interfaces;
using OneForAll.Core.Extension;
using OneForAll.Core.OAuth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 人事历程
    /// </summary>
    public class OAPersonalTeamHistoryService : IOAPersonalTeamHistoryService
    {
        private readonly IMapper _mapper;
        private readonly IOATeamMemberHistoryQueryService _queryService;
        public OAPersonalTeamHistoryService(IMapper mapper, IOATeamMemberHistoryQueryService queryService)
        {
            _mapper = mapper;
            _queryService = queryService;
        }

        /// <summary>
        /// 获取人事历程列表
        /// </summary>
        /// <param name="loginUser">登录用户</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamMemberHistoryDto>> GetListAsync(LoginUser loginUser)
        {
            var data = await _queryService.GetListByUserIdAsync(loginUser.Id.TryGuid());
            return _mapper.Map<IEnumerable<OATeamMemberHistory>, IEnumerable<OATeamMemberHistoryDto>>(data);
        }
    }
}
