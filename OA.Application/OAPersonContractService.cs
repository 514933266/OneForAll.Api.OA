using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 合同管理
    /// </summary>
    public class OAPersonContractService : IOAPersonContractService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonContractManager _manager;
        public OAPersonContractService(
            IMapper mapper,
            IOAPersonContractManager manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        /// <summary>
        /// 获取近二个月合同即将过期/已过期列表
        /// </summary>
        /// <param name="teamId">所属部门id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonContractDto>> GetListAsync(Guid teamId)
        {
            var data = await _manager.GetListAsync(teamId);
            return _mapper.Map<IEnumerable<OAPersonContractAggr>, IEnumerable<OAPersonContractDto>>(data);
        }
    }
}
