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
using OA.Domain.Models;

namespace OA.Application
{
    /// <summary>
    /// 人员资料
    /// </summary>
    public class OAJobDutyService : IOAJobDutyService
    {
        private readonly IMapper _mapper;
        private readonly IOAJobDutyManager _manager;
        public OAJobDutyService(
            IMapper mapper,
            IOAJobDutyManager manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<IEnumerable<OAJobDutyDto>> GetListAsync(string key)
        {
            var data = await _manager.GetListAsync(key);
            return _mapper.Map<IEnumerable<OAJobDuty>, IEnumerable<OAJobDutyDto>>(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAJobDutyForm form)
        {
            return await _manager.AddAsync(form);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAJobDutyForm form)
        {
            return await _manager.UpdateAsync(form);
        }

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _manager.DeleteAsync(ids);
        }
    }
}
