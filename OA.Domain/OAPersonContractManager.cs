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
        public OAPersonContractManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOATeamPersonContactRepository contactRepository) : base(mapper, httpContextAccessor)
        {
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// 获取近二个月合同即将过期/已过期列表
        /// </summary>
        /// <param name="teamId">所属部门id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonContractAggr>> GetListAsync(Guid teamId)
        {
            var result = new List<OAPersonContractAggr>();
            var data = await _contactRepository.GetListByTeamAsync(teamId, string.Empty);
            data.ForEach(e =>
            {
                if (e.LeaveDate == null)
                {
                    if (!e.ExtendInformationJson.IsNullOrEmpty())
                    {
                        var infos = e.ExtendInformationJson.FromJson<List<OAPersonExtenInformationFieldVo>>();
                        var starDate = infos.FirstOrDefault(w => w.Name == new OAPersonContractStartDateVo().Name);
                        var endDate = infos.FirstOrDefault(w => w.Name == new OAPersonContractEndDateVo().Name);
                        var company = infos.FirstOrDefault(w => w.Name == new OAPersonContractCompanyVo().Name);
                        var type = infos.FirstOrDefault(w => w.Name == new OAPersonContractTypeVo().Name);
                        if (endDate != null && !endDate.Value.IsNullOrEmpty() && (endDate.Value.TryDateTime() - DateTime.Now).TotalDays <= 60)
                        {
                            var contractInfo = _mapper.Map<OAPersonContractAggr>(e);
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
                }
            });
            return result;
        }
    }
}
