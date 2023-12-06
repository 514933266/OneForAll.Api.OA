using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 团队成员异动历史
    /// </summary>
    public class OATeamMemberHistoryService : IOATeamMemberHistoryService
    {
        private readonly IMapper _mapper;
        private readonly IOATeamMemberHistoryRepository _repository;
        public OATeamMemberHistoryService(IMapper mapper, IOATeamMemberHistoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="teamId"></param>
        /// <param name="key"></param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>结果</returns>
        public async Task<PageList<OATeamMemberHistoryDto>> GetPageAsync(
            int pageIndex,
            int pageSize,
            Guid teamId,
            string key,
            DateTime? startDate,
            DateTime? endDate)
        {
            var data = await _repository.GetPageAsync(pageIndex, pageSize, teamId, key, startDate, endDate);
            var items = _mapper.Map<IEnumerable<OATeamMemberHistory>, IEnumerable<OATeamMemberHistoryDto>>(data.Items);
            return new PageList<OATeamMemberHistoryDto>(data.Total, data.PageIndex, data.PageSize, items);
        }
    }
}
