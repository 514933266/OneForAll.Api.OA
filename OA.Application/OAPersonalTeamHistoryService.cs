using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Public.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 人事历程
    /// </summary>
    public class OAPersonalTeamHistoryService : IOAPersonalTeamHistoryService
    {
        private readonly IMapper _mapper;
        private readonly IOATeamMemberHistoryRepository _repository;
        public OAPersonalTeamHistoryService(IMapper mapper, IOATeamMemberHistoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 获取人事历程列表
        /// </summary>
        /// <param name="loginUser">登录用户</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamMemberHistoryDto>> GetListAsync(LoginUser loginUser)
        {
            var data = await _repository.GetListByUserIdAsync(loginUser.Id);
            return _mapper.Map<IEnumerable<OATeamMemberHistory>, IEnumerable<OATeamMemberHistoryDto>>(data);
        }
    }
}
