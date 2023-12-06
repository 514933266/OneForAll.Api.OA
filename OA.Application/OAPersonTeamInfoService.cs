using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.Aggregates;
using OA.Domain.Enums;
using OA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 人员团队信息
    /// </summary>
    public class OAPersonTeamInfoService : IOAPersonTeamInfoService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonTeamInfoManager _manager;
        private readonly IOATeamManager _teamManager;
        public OAPersonTeamInfoService(
            IMapper mapper,
            IOAPersonTeamInfoManager manager,
            IOATeamManager teamManager)
        {
            _mapper = mapper;
            _manager = manager;
            _teamManager = teamManager;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OAPersonTeamInfoDto>> GetListAsync(string key)
        {
            var teams = await _teamManager.GetListAsync(Guid.Empty, string.Empty, true, OATeamSearchScopeEnum.None);
            var data = await _manager.GetListAsync(teams, key);
            return _mapper.Map<IEnumerable<OAPersonTeamInfoAggr>, IEnumerable<OAPersonTeamInfoDto>>(data);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        public async Task<OAPersonTeamInfoDto> GetAsync(Guid id)
        {
            var teams = await _teamManager.GetListAsync(Guid.Empty, string.Empty, true, OATeamSearchScopeEnum.None);
            var data = await _manager.GetAsync(teams, id);
            return _mapper.Map<OAPersonTeamInfoAggr, OAPersonTeamInfoDto>(data);
        }
    }
}
