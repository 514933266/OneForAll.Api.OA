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
using OA.Domain.Aggregates;
using OneForAll.Core.Extension;

namespace OA.Application
{
    /// <summary>
    /// 员工生日
    /// </summary>
    public class OAPersonCareService : IOAPersonBirthdayCareService
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
            var data = await _manager.GetListBirthdayAsync(teamId, startDate, endDate);

            var items = _mapper.Map<IEnumerable<OATeamMemberAggr>, IEnumerable<OAPersonBirthdayCareDto>>(data);
            foreach (var item in items)
            {
                var person = data.FirstOrDefault(w => w.Id == item.Id);
                if (person != null && person.OATeam != null)
                {
                    item.Teams = new List<OAPersonTeamDto>()
                    {
                        new OAPersonTeamDto()
                        {
                             IsLeader = person.Contact.IsLeader,
                             Name = person.OATeam.Name,
                             Type = person.OATeam.Type
                        }
                    };
                }
            }
            return items;
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
            var data = await _manager.GetListCompanyAsync(teamId, date);

            var items = _mapper.Map<IEnumerable<OATeamMemberAggr>, IEnumerable<OAPersonCompanyCareDto>>(data);
            foreach (var item in items)
            {
                var person = data.FirstOrDefault(w => w.Id == item.Id);
                if (person != null && person.OATeam != null)
                {
                    item.Teams = new List<OAPersonTeamDto>()
                    {
                        new OAPersonTeamDto()
                        {
                             IsLeader = person.Contact.IsLeader,
                             Name = person.OATeam.Name,
                             Type = person.OATeam.Type
                        }
                    };
                }
            }
            return items;
        }
    }
}
