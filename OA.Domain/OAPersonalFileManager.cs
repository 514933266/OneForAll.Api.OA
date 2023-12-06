using AutoMapper;
using Microsoft.AspNetCore.Http;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.ValueObjects;
using OneForAll.Core;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain
{
    /// <summary>
    /// 个人档案
    /// </summary>
    public class OAPersonalFileManager : OABaseManager, IOAPersonalFileManager
    {
        private readonly IOAPersonRepository _repository;
        private readonly IOAPersonSettingRepository _settingRepository;
        private readonly IOAPersonSettingFieldRepository _settingFieldRepository;
        public OAPersonalFileManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOAPersonRepository repository,
            IOAPersonSettingRepository settingRepository,
            IOAPersonSettingFieldRepository settingFieldRepository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
            _settingRepository = settingRepository;
            _settingFieldRepository = settingFieldRepository;
        }

        /// <summary>
        /// 获取个人档案
        /// </summary>
        public async Task<OAPerson> GetAsync()
        {
            var data = await _repository.GetAsync(w => w.SysUserId == LoginUser.Id);
            if (data == null)
                return new OAPerson();

            var infos = data.ExtendInformationJson.FromJson<List<OAPersonExtenInformationFieldVo>>();
            // 个人档案要过滤管理员设置不显示的内容
            var settings = await _settingRepository.GetListAsync(w => w.IsEnabled);
            if (settings.Any())
            {
                var sids = settings.Select(s => s.Id).ToList();
                var fields = await _settingFieldRepository.GetListAsync(w => sids.Contains(w.OAPersonSettingId) && w.IsEmployeeVisiable);
                var names = fields.Select(s => s.Name).ToList();
                if (names.Any())
                {
                    infos = infos.Where(w => names.Contains(w.Name)).ToList();
                }
                else
                {
                    infos.Clear();
                }
                data.ExtendInformationJson = infos.ToJson();
            }
            return data;
        }

        /// <summary>
        /// 绑定档案
        /// </summary>
        /// <param name="key">身份证 或 手机号码</param>
        /// <returns></returns>
        public async Task<BaseErrType> BindAsync(string key)
        {
            var data = await _repository.GetAsync(w => w.MobilePhone == key || w.IdCard == key);
            if (data == null)
                return BaseErrType.DataNotFound;
            if (data.SysUserId != Guid.Empty && data.SysUserId != LoginUser.Id)
                return BaseErrType.NotAllow;

            data.SysUserId = LoginUser.Id;

            return await ResultAsync(_repository.SaveChangesAsync);
        }
    }
}
