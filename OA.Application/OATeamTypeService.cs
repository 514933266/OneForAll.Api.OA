using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Interfaces;
using OneForAll.Core;

namespace OA.Application
{
    /// <summary>
    /// 团队类型
    /// </summary>
    public class OATeamTypeService : IOATeamTypeService
    {
        private readonly IMapper _mapper;
        private readonly IOATeamTypeManager _manager;

        public OATeamTypeService(
            IMapper mapper,
            IOATeamTypeManager manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        /// <summary>
        /// 获取类型列表
        /// </summary>
        /// <param name="name">类型</param>
        /// <returns>组织架构树</returns>
        public async Task<IEnumerable<OATeamTypeDto>> GetListAsync(string name)
        {
            var data = await _manager.GetListAsync(name);
            return _mapper.Map<IEnumerable<OATeamType>, IEnumerable<OATeamTypeDto>>(data);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>列表</returns>
        public async Task<BaseErrType> DeleteAsync(Guid id)
        {
            return await _manager.DeleteAsync(id);
        }
    }
}
