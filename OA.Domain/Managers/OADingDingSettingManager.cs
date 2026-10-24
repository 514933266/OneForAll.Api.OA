using AutoMapper;
using OA.Domain.AggregateRoots;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OA.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Managers
{
    /// <summary>
    /// 钉钉接入设置
    /// </summary>
    public class OADingDingSettingManager : OADomainManager, IOADingDingSettingManager
    {
        private readonly IMapper _mapper;
        private readonly IOADingDingSettingRepository _repository;
        public OADingDingSettingManager(
            IMapper mapper,
            ICurrentUser currentUser,
            IOADingDingSettingRepository repository) : base(currentUser)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns>列表</returns>
        public async Task<OADingDingSetting> GetAsync()
        {
            return await _repository.GetAsync();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OADingDingSettingForm form)
        {
            var count = await _repository.CountAsync();
            if (count > 0) return BaseErrType.DataExist;

            var data = _mapper.Map<OADingDingSettingForm,OADingDingSetting>(form);
            data.SysTenantId = LoginUser.TenantId.TryGuid().TryGuid();
            data.CreatorId = LoginUser.Id.TryGuid().TryGuid();
            data.CreatorName= LoginUser.DisplayName;

            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OADingDingSettingForm form)
        {
            var data = await _repository.FindAsync(form.Id);
            if (data == null)
                return BaseErrType.DataNotFound;

            _mapper.Map<OADingDingSettingForm, OADingDingSetting>(form, data);
            data.CreatorId = LoginUser.Id.TryGuid().TryGuid();
            data.CreatorName = LoginUser.DisplayName;
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }
    }
}
