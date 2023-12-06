using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OneForAll.Core;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 个人日程
    /// </summary>
    public class OAPersonalScheduleService : IOAPersonalScheduleService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonalScheduleManager _manager;
        public OAPersonalScheduleService(
            IMapper mapper,
            IOAPersonalScheduleManager manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="date">月份</param>
        /// <param name="isClosed">是否已关闭</param>
        /// <returns>分页列表</returns>
        public async Task<IEnumerable<OAPersonalScheduleDto>> GetListAsync(DateTime date, bool? isClosed)
        {
            var data = await _manager.GetListAsync(date, isClosed);
            return _mapper.Map<IEnumerable<OAPersonalSchedule>, IEnumerable<OAPersonalScheduleDto>>(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAPersonalScheduleForm form)
        {
            return await _manager.AddAsync(form);
        }

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">项目id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _manager.DeleteAsync(ids);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="id">日程id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> CloseAsync(Guid id)
        {
            return await _manager.CloseAsync(id);
        }
    }
}
