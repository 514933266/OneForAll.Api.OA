using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OA.Domain.Interfaces;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.Aggregates;

namespace OA.Application
{
    /// <summary>
    /// 员工生日
    /// </summary>
    public class OAPersonCareService : IOAPersonBirthdayCareService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonCareQueryService _careQueryService;

        public OAPersonCareService(
            IMapper mapper,
            IOAPersonCareQueryService careQueryService)
        {
            _mapper = mapper;
            _careQueryService = careQueryService;
        }

        /// <summary>
        /// 获取员工生日列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonBirthdayCareDto>> GetListBirthdayAsync(Guid teamId, DateTime startDate, DateTime endDate)
        {
            var data = await _careQueryService.GetListBirthdayAsync(teamId, startDate, endDate);
            // 所在部门由 Profile 从聚合根导航属性投影
            return _mapper.Map<IEnumerable<OATeamMemberAggr>, IEnumerable<OAPersonBirthdayCareDto>>(data);
        }

        /// <summary>
        /// 获取入职周年列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="date">日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonCompanyCareDto>> GetListCompanyAsync(Guid teamId, DateTime date)
        {
            var data = await _careQueryService.GetListCompanyAsync(teamId, date);
            // 所在部门由 Profile 从聚合根导航属性投影
            return _mapper.Map<IEnumerable<OATeamMemberAggr>, IEnumerable<OAPersonCompanyCareDto>>(data);
        }
    }
}
