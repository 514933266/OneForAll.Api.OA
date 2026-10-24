using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OneForAll.Core;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Querys
{
    /// <summary>
    /// 团队成员异动历史查询服务
    /// </summary>
    public class OATeamMemberHistoryQueryService : IOATeamMemberHistoryQueryService
    {
        private readonly IMapper _mapper;
        private readonly IOATeamMemberHistoryRepository _repository;

        public OATeamMemberHistoryQueryService(
            IMapper mapper,
            IOATeamMemberHistoryRepository repository)
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
        public async Task<PageList<OATeamMemberHistory>> GetPageAsync(
            int pageIndex,
            int pageSize,
            Guid teamId,
            string key,
            DateTime? startDate,
            DateTime? endDate)
        {
            var data = await _repository.GetPageAsync(pageIndex, pageSize, teamId, key, startDate, endDate);
            return _mapper.Map<PageList<OATeamMemberHistory>>(data);
        }

        /// <summary>
        /// 按登录用户id获取人事历程列表
        /// </summary>
        /// <param name="userId">登录用户id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamMemberHistory>> GetListByUserIdAsync(Guid userId)
        {
            return await _repository.GetListByUserIdAsync(userId);
        }
    }
}
