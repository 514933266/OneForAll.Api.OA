using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OneForAll.Core;
using OA.Domain.Enums;
using OA.Domain.Interfaces;
using OA.Application.Dtos;
using OA.Domain.AggregateRoots;
using OA.Application.Interfaces;

namespace OA.Application
{
    /// <summary>
    /// 员工生日
    /// </summary>
    public class OAPersonCareService: IOAPersonBirthdayCareService
    {
        private readonly IMapper _mapper;
        private readonly IOATeamManager _teamManager;
        private readonly IOAPersonCareManager _manager;
        public OAPersonCareService(
            IMapper mapper,
            IOATeamManager teamManager,
            IOAPersonCareManager manager)
        {
            _mapper = mapper;
            _manager = manager;
            _teamManager = teamManager;
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
            var teams = await _teamManager.GetListAsync(Guid.Empty, string.Empty, true, OATeamSearchScopeEnum.Valid);
            var data = await _manager.GetListBirthdayAsync(teamId, startDate, endDate, teams);

            return _mapper.Map<IEnumerable<OAPerson>, IEnumerable<OAPersonBirthdayCareDto>>(data);
        }

        /// <summary>
        /// 获取入职周年列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="date">日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonCompanyCareDto>> GetListCompanyAsync(Guid teamId, DateTime date)
        {
            var teams = await _teamManager.GetListAsync(Guid.Empty, string.Empty, true, OATeamSearchScopeEnum.Valid);
            var data = await _manager.GetListCompanyAsync(teamId, date, teams);

            return _mapper.Map<IEnumerable<OAPerson>, IEnumerable<OAPersonCompanyCareDto>>(data);
        }
    }
}
