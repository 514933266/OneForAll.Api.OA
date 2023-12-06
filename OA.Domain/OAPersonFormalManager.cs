using AutoMapper;
using Microsoft.AspNetCore.Http;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Enums;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OA.Domain.Repositorys;
using OA.Domain.ValueObjects;
using OneForAll.Core;
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
    /// 员工转正管理
    /// </summary>
    public class OAPersonFormalManager : OABaseManager, IOAPersonFormalManager
    {
        private readonly IOATeamPersonContactRepository _contactRepository;
        private readonly IOAPersonRepository _personRepository;
        public OAPersonFormalManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOATeamPersonContactRepository contactRepository,
            IOAPersonRepository personRepository) : base(mapper, httpContextAccessor)
        {
            _contactRepository = contactRepository;
            _personRepository = personRepository;
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
                        info.PlanEntryDate = planDate.Value.TryDateTime();
                    if (actualDate != null)
                        info.ActualEntryDate = actualDate.Value.TryDateTime();

                    if (planStartDate != null && info.PlanEntryDate < planStartDate || info.PlanEntryDate > planEndDate)
                        return;
                    if (actualStartDate != null && info.ActualEntryDate < actualStartDate || info.ActualEntryDate > actualEndDate)
                        return;

                    result.Add(info);
                }
            });
            return result;
        }

        /// <summary>
        /// 办理转正
        /// </summary>
        /// <param name="id">数据id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> ConfirmAsync(Guid id, OAPersonFormalConfirmForm form)
        {
            var data = await _personRepository.FindAsync(id);
            if (data == null) return BaseErrType.DataNotFound;

            var statusVal = OAPersonEmployeeStatusVo.GetDefaultSelection().Value;
            data.EmployeeStatus = statusVal;
            var infos = data.ExtendInformationJson.FromJson<List<OAPersonExtenInformationFieldVo>>();
            if (infos.Any())
            {
                var status = infos.FirstOrDefault(w => w.Name == new OAPersonEmployeeStatusVo().Name);
                var date = infos.FirstOrDefault(w => w.Name == new OAPersonActualEntryDateVo().Name);
                if (status != null)
                    status.Value = statusVal;
                if (date != null)
                    date.Value = form.ActualEntryDate.ToString();

                data.ExtendInformationJson = infos.ToJson();
            }
            return await ResultAsync(() => _personRepository.UpdateAsync(data));
        }
    }
}
