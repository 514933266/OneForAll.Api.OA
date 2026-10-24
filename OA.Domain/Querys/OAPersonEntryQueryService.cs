using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OneForAll.Core;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.Aggregates;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Querys
{
    /// <summary>
    /// 员工入职查询服务
    /// </summary>
    public class OAPersonEntryQueryService : IOAPersonEntryQueryService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonEntryRepository _repository;

        public OAPersonEntryQueryService(
            IMapper mapper,
            IOAPersonEntryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<OAPersonEntryAggr>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var data = await _repository.GetPageAsync(pageIndex, pageSize, key);
            var items = _mapper.Map<IEnumerable<OAPersonEntry>, IEnumerable<OAPersonEntryAggr>>(data.Items);
            return new PageList<OAPersonEntryAggr>(data.Total, data.PageIndex, data.PageSize, items);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="creatorName">创建人</param>
        /// <param name="mobilePhone">手机号码</param>
        /// <param name="startDate">开始入职日期</param>
        /// <param name="endDate">开始入职日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonEntryAggr>> GetListAsync(
             string name,
             string creatorName,
             string mobilePhone,
             DateTime? startDate,
             DateTime? endDate)
        {
            var data = await _repository.GetListAsync(name, creatorName, mobilePhone, startDate, endDate);
            return _mapper.Map<IEnumerable<OAPersonEntry>, IEnumerable<OAPersonEntryAggr>>(data);
        }

        /// <summary>
        /// 查询指定id数据
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        public async Task<OAPersonEntryAggr> GetAsync(Guid id)
        {
            var data = await _repository.FindAsync(id);
            if (data == null) return null;
            return _mapper.Map<OAPersonEntry, OAPersonEntryAggr>(data);
        }
    }
}
