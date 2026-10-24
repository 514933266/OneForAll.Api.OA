using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.Aggregates;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Querys
{
    /// <summary>
    /// 新员工入职档案查询服务
    /// </summary>
    public class OAPersonEntryFileQueryService : IOAPersonEntryFileQueryService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonEntryRepository _repository;
        private readonly IOAPersonSettingRepository _settingRepository;
        private readonly IOAPersonSettingFieldRepository _fieldRepository;

        public OAPersonEntryFileQueryService(
            IMapper mapper,
            IOAPersonEntryRepository repository,
            IOAPersonSettingRepository settingRepository,
            IOAPersonSettingFieldRepository fieldRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _settingRepository = settingRepository;
            _fieldRepository = fieldRepository;
        }

        /// <summary>
        /// 获取档案设置列表
        /// </summary>
        /// <param name="id">入职档案id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonSettingAggr>> GetListSettingAsync(Guid id)
        {
            var data = await _repository.GetIQFAsync(id);
            if (data == null) return new List<OAPersonSettingAggr>();

            var settings = await _settingRepository.GetListByTenantIQFAsync(data.SysTenantId);
            // 只显示启用并允许编辑的设置
            settings = settings.Where(w => w.IsEnabled).ToList();
            var ids = settings.Select(s => s.Id).ToList();
            var fields = await _fieldRepository.GetListBySettingAsync(ids);


            var result = _mapper.Map<List<OAPersonSettingAggr>>(settings);
            result.ForEach(e =>
            {
                e.OAPersonSettingFields = fields.Where(w => w.OAPersonSettingId == e.Id && w.IsEnabled && w.IsEntryFileVisiable).OrderBy(o => o.SortNumber).ToList();
            });
            return result.OrderBy(o => o.SortNumber).ToList();
        }

        /// <summary>
        /// 获取入职登记实体
        /// </summary>
        /// <param name="id">入职档案id</param>
        /// <returns>实体</returns>
        public async Task<OAPersonEntry> GetAsync(Guid id)
        {
            return await _repository.GetIQFAsync(id);
        }
    }
}
