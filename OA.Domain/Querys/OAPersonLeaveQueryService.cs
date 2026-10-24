using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.Aggregates;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Querys
{
    /// <summary>
    /// 离职登记查询服务
    /// </summary>
    public class OAPersonLeaveQueryService : IOAPersonLeaveQueryService
    {
        private readonly IOAPersonLeaveRepository _repository;

        public OAPersonLeaveQueryService(
            IOAPersonLeaveRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<OAPersonLeave>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            return await _repository.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="creatorName">创建人</param>
        /// <param name="teamId">部门</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonLeaveAggr>> GetListAsync(
             string name,
             string creatorName,
             Guid? teamId,
             DateTime? startDate,
             DateTime? endDate)
        {
            return await _repository.GetListAsync(name, creatorName, teamId, startDate, endDate);
        }

        /// <summary>
        /// 查询指定id的离职登记
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        public async Task<OAPersonLeave> GetAsync(Guid id)
        {
            return await _repository.FindAsync(id);
        }
    }
}
