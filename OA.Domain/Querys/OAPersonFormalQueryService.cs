using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OneForAll.Core.Extension;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.Aggregates;
using OA.Domain.ValueObjects;

namespace OA.Domain.Querys
{
    /// <summary>
    /// 员工转正查询服务
    /// </summary>
    public class OAPersonFormalQueryService : IOAPersonFormalQueryService
    {
        private readonly IMapper _mapper;
        private readonly IOATeamPersonContactRepository _contactRepository;

        public OAPersonFormalQueryService(
            IMapper mapper,
            IOATeamPersonContactRepository contactRepository)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// 获取待转正列表
        /// </summary>
        /// <param name="name">员工姓名</param>
        /// <param name="teamId">团队id</param>
        /// <param name="planStartDate">计划转正-开始日期</param>
        /// <param name="planEndDate">计划转正-开始日期</param>
        /// <param name="actualStartDate">实际转正-开始日期</param>
        /// <param name="actualEndDate">实际转正-开始日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonFormalAggr>> GetListAsync(
            string name,
            Guid teamId,
            DateTime? planStartDate,
            DateTime? planEndDate,
            DateTime? actualStartDate,
            DateTime? actualEndDate)
        {
            var result = new List<OAPersonFormalAggr>();
            var data = await _contactRepository.GetListByTeamAsync(teamId, name);
            data.ForEach(e =>
            {
                if (actualStartDate == null && !e.EmployeeStatus.Contains("试用"))
                    return;

                if (!e.ExtendInformationJson.IsNullOrEmpty())
                {
                    var infos = e.ExtendInformationJson.FromJson<List<OAPersonExtenInformationFieldVo>>();
                    var tryDate = infos.FirstOrDefault(w => w.Name == new OAPersonTryDateVo().Name);
                    var planDate = infos.FirstOrDefault(w => w.Name == new OAPersonPlanEntryDateVo().Name);
                    var actualDate = infos.FirstOrDefault(w => w.Name == new OAPersonActualEntryDateVo().Name);

                    var info = _mapper.Map<OAPersonFormalAggr>(e);

                    if (tryDate != null)
                        info.TryDate = tryDate.Value;
                    if (planDate != null)
                        info.PlanEntryDate = planDate.Value.IsNullOrEmpty() ? null : planDate.Value.TryDateTime();
                    if (actualDate != null)
                        info.ActualEntryDate = actualDate.Value.IsNullOrEmpty() ? null : actualDate.Value.TryDateTime();

                    if (planStartDate != null && info.PlanEntryDate < planStartDate || info.PlanEntryDate > planEndDate)
                        return;
                    if (actualStartDate != null && info.ActualEntryDate < actualStartDate || info.ActualEntryDate > actualEndDate)
                        return;

                    result.Add(info);
                }
            });
            return result;
        }
    }
}
