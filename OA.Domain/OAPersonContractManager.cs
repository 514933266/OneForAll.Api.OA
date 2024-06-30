using AutoMapper;
using Microsoft.AspNetCore.Http;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Enums;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.ValueObjects;
using OneForAll.Core.Extension;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain
{
    /// <summary>
    /// 员工合同管理
    /// </summary>
    public class OAPersonContractManager : OABaseManager, IOAPersonContractManager
    {
        private readonly IOATeamPersonContactRepository _contactRepository;
        private readonly IOAPersonSettingRepository _settingRepository;
        private readonly IOAPersonSettingFieldRepository _settingFieldRepository;
        public OAPersonContractManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOATeamPersonContactRepository contactRepository,
            IOAPersonSettingRepository settingRepository,
            IOAPersonSettingFieldRepository settingFieldRepository) : base(mapper, httpContextAccessor)
        {
            _contactRepository = contactRepository;
            _settingRepository = settingRepository;
            _settingFieldRepository = settingFieldRepository;
        }

        /// <summary>
        /// 获取近二个月合同即将过期/已过期列表
        /// </summary>
        /// <param name="teamId">所属部门id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonContractAggr>> GetListAsync(Guid teamId)
        {
            var result = new List<OAPersonContractAggr>();
            var companySetting = await _settingRepository.GetAsync(w => w.Type == OAPersonSettingTypeEnum.ContractInformation);
            var data = await _contactRepository.GetListByTeamAsync(teamId, string.Empty);

            if (companySetting != null)
            {
                var fields = await _settingFieldRepository.GetListBySettingAsync(companySetting.Id);
                if (!fields.Any()) return result;

                data.ForEach(e =>
                {
                    if (e.LeaveDate != null) return;
                    if (e.ExtendInformationJson.IsNullOrEmpty()) return;
                    var infos = e.ExtendInformationJson.FromJson<List<OAPersonExtenInformationFieldVo>>().OrderBy(o => o.Name);

                    var endDateName = new OAPersonContractEndDateVo().Name;
                    var starDateName = new OAPersonContractStartDateVo().Name;
                    var companyName = new OAPersonContractCompanyVo().Name;
                    var typeName = new OAPersonContractTypeVo().Name;

                    var total = fields.Where(w => w.Name.StartsWith(endDateName)).Count();
                    for (var i = 0; i < total; i++)
                    {
                        var suffix = i == 0 ? "" : i.ToString();
                        var endDate = infos.FirstOrDefault(w => w.Name == endDateName.Append(suffix));

                        if (endDate != null && !endDate.Value.IsNullOrEmpty() && (endDate.Value.TryDateTime() - DateTime.Now).TotalDays <= 60)
                        {
                            var contractInfo = _mapper.Map<OAPersonContractAggr>(e);
                            var starDate = infos.FirstOrDefault(w => w.Name == starDateName.Append(suffix));
                            var company = infos.FirstOrDefault(w => w.Name == companyName.Append(suffix));
                            var type = infos.FirstOrDefault(w => w.Name == typeName.Append(suffix));
                            if (starDate != null)
                                contractInfo.ContractFirstDate = starDate.Value.TryDateTime();
                            if (endDate != null)
                                contractInfo.ContractEndDate = endDate.Value.TryDateTime();
                            if (company != null)
                                contractInfo.ContractCompany = company.Value;
                            if (type != null)
                                contractInfo.ContractType = type.Value;
                            result.Add(contractInfo);
                        }
                    }
                });
            }
            return result;
        }
    }
}
