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
using OA.Domain.Aggregates;
using OneForAll.EFCore;
using OA.Domain.Repositorys;
using OneForAll.Core.Extension;
using OneForAll.Core.Utility;
using OA.Public.Models;
using System.Runtime.InteropServices;
using OA.Domain;
using OA.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace OA.Application
{
    /// <summary>
    /// 人员资料
    /// </summary>
    public class OAPersonEntryService : IOAPersonEntryService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonEntryManager _manager;
        private readonly IOAPersonManager _personManager;
        private readonly IOATeamMemberManager _memberManager;
        private readonly IOATeamMemberHistoryManager _memberHistoryManager;

        private readonly IOAPersonEntryRepository _repository;
        private readonly IOAPersonRepository _personRepository;
        public OAPersonEntryService(
            IMapper mapper,
            IOAPersonEntryManager manager,
            IOAPersonManager personManager,
            IOATeamMemberManager memberManager,
            IOATeamMemberHistoryManager memberHistoryManager,
            IOAPersonEntryRepository repository,
            IOAPersonRepository personRepository)
        {
            _mapper = mapper;
            _manager = manager;
            _personManager = personManager;
            _memberManager = memberManager;
            _memberHistoryManager = memberHistoryManager;
            _repository = repository;
            _personRepository = personRepository;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="creatorName">创建人</param>
        /// <param name="mobilePhone">手机号码</param>
        /// <param name="startDate">开始入职日期</param>
        /// <param name="endDate">开始入职日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonEntryDto>> GetListAsync(
             string name,
             string creatorName,
             string mobilePhone,
             DateTime? startDate,
             DateTime? endDate)
        {
            var data = await _manager.GetListAsync(name, creatorName, mobilePhone, startDate, endDate);
            var items = _mapper.Map<IEnumerable<OAPersonEntry>, IEnumerable<OAPersonEntryDto>>(data);
            items.ForEach(e =>
            {
                var days = (DateTime.Now - e.EstimateEntryDate).TotalDays;
                if (days >= 1)
                {
                    e.IsOverdue = true;
                    e.OverdueDays = (int)days;
                }
            });
            return items;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<OAPersonEntryDto>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var data = await _manager.GetPageAsync(pageIndex, pageSize, key);
            var items = _mapper.Map<IEnumerable<OAPersonEntry>, IEnumerable<OAPersonEntryDto>>(data.Items);
            return new PageList<OAPersonEntryDto>(data.Total, data.PageIndex, data.PageSize, items);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAPersonEntryForm form)
        {
            return await _manager.AddAsync(form);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAPersonEntryForm form)
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

        /// <summary>
        /// 确认到岗
        /// </summary>
        /// <param name="id">数据id</param>
        /// <param name="form">人员信息表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> ConfirmAsync(Guid id, OAPersonEntryConfirmForm form)
        {
            var data = await _repository.FindAsync(id);
            if (data == null) return BaseErrType.DataNotFound;
            if (!data.IsSubmitEntryFile) return BaseErrType.NotAllow;

            var info = _mapper.Map<OAPersonEntryConfirmForm, OAPersonForm>(form);
            if (data.IsSubmitEntryFile && !data.ExtendInformationJson.IsNullOrWhiteSpace())
            {
                info.ExtendInformations = data.ExtendInformationJson.FromJson<List<OAPersonExtenInformationFieldVo>>();
                info.IdCard = info.ExtendInformations == null ? "" : info.ExtendInformations.FirstOrDefault(w => w.Name == new OAPersonIdCardVo().Name)?.Value;
            }

            #region 默认信息填充到附加信息中

            var entryDate = DateTime.Now.ToString("yyyy-MM-dd");
            var workNumberName = new OAPersonWorkNumberVo().Name;
            var entryDateName = new OAPersonEntryDateVo().Name;
            var employeeStatusName = new OAPersonEmployeeStatusVo().Name;
            var employeeTypeName = new OAPersonEmployeeTypeVo().Name;
            var remarkName = new OAPersonRemarkVo().Name;

            var workNumberItem = info.ExtendInformations.FirstOrDefault(w => w.Name == workNumberName);
            if (workNumberItem == null)
            {
                info.ExtendInformations.Add(new OAPersonExtenInformationFieldVo() { Name = workNumberName, Value = info.WorkNumber });
            }
            else
            {
                workNumberItem.Value = info.WorkNumber;
            }

            var entryDateItem = info.ExtendInformations.FirstOrDefault(w => w.Name == entryDateName);
            if (entryDateItem == null)
            {
                info.ExtendInformations.Add(new OAPersonExtenInformationFieldVo() { Name = entryDateName, Value = entryDate });
            }
            else
            {
                entryDateItem.Value = entryDate;
            }

            var employeeStatusItem = info.ExtendInformations.FirstOrDefault(w => w.Name == employeeStatusName);
            if (employeeStatusItem == null)
            {
                info.ExtendInformations.Add(new OAPersonExtenInformationFieldVo() { Name = employeeStatusName, Value = info.EmployeeStatus });
            }
            else
            {
                employeeStatusItem.Value = info.EmployeeStatus;
            }

            var employeeTypeItem = info.ExtendInformations.FirstOrDefault(w => w.Name == employeeTypeName);
            if (employeeTypeItem == null)
            {
                info.ExtendInformations.Add(new OAPersonExtenInformationFieldVo() { Name = employeeTypeName, Value = info.EmployeeType });
            }
            else
            {
                employeeTypeItem.Value = info.EmployeeType;
            }

            var remarkItem = info.ExtendInformations.FirstOrDefault(w => w.Name == remarkName);
            if (remarkItem == null)
            {
                info.ExtendInformations.Add(new OAPersonExtenInformationFieldVo() { Name = remarkName, Value = info.Remark });
            }
            else
            {
                remarkItem.Value = info.Remark;
            }
            if (info.EmployeeStatus == "试用员工")
            {
                var tryDate = new OAPersonTryDateVo().Name;
                var planEntryDateName = new OAPersonPlanEntryDateVo().Name;

                var tryDateItem = info.ExtendInformations.FirstOrDefault(w => w.Name == tryDate);
                if (tryDateItem == null)
                {
                    info.ExtendInformations.Add(new OAPersonExtenInformationFieldVo() { Name = tryDate, Value = "3个月" });
                }
                else
                {
                    tryDateItem.Value = tryDate;
                }

                var planEntryDateItem = info.ExtendInformations.FirstOrDefault(w => w.Name == planEntryDateName);
                if (planEntryDateItem == null)
                {
                    info.ExtendInformations.Add(new OAPersonExtenInformationFieldVo() { Name = planEntryDateName, Value = entryDate.TryDateTime().AddMonths(3).ToString("yyyy-MM-dd") });
                }
                else
                {
                    planEntryDateItem.Value = entryDate.TryDateTime().AddMonths(3).ToString("yyyy-MM-dd");
                }
            }

            #endregion

            // 1. 创建人员档案
            info.Id = data.Id;// 将入职资料的id传递给人员档案，直接将上传的附件同步而不需要转移文件
            var errType = await _personManager.AddAsync(info);
            if (errType == BaseErrType.Success || errType == BaseErrType.DataExist)
            {
                var person = await _personRepository.GetAsync(w => w.Name == data.Name && w.IdCard == info.IdCard);
                if (person != null)
                {
                    // 2. 加入团队
                    var errType2 = await _memberManager.AddAsync(data.TeamId, new List<Guid>() { person.Id });
                    // 3. 生成异动日志
                    if (errType2 == BaseErrType.Success)
                    {
                        await _memberHistoryManager.AddAsync(new OATeamMemberForm()
                        {
                            TeamId = data.TeamId,
                            Id = person.Id,
                            Name = person.Name,
                            Job = person.Job,
                            IdCard = person.IdCard,
                            Remark = "办理入职"
                        });
                    }
                }
            }

            // 4. 删除登记
            if (errType == BaseErrType.Success)
            {
                return await _manager.ConfirmAsync(id);
            }

            return errType;
        }
    }
}
