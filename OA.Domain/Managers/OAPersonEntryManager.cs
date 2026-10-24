using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.Aggregates;
using OA.Domain.ValueObjects;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Managers
{
    /// <summary>
    /// 员工入职
    /// </summary>
    public class OAPersonEntryManager : OADomainManager, IOAPersonEntryManager
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonEntryRepository _repository;
        private readonly IOATeamRepository _teamRepository;

        public OAPersonEntryManager(
            IMapper mapper,
            ICurrentUser currentUser,
            IOAPersonEntryRepository repository,
            IOATeamRepository teamRepository) : base(currentUser)
        {
            _mapper = mapper;
            _repository = repository;
            _teamRepository = teamRepository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAPersonEntryForm form)
        {
            var exists = await _repository.GetByNameAndMobileAsync(form.Name, form.MobilePhone);
            if (exists != null)
                return BaseErrType.DataExist;

            var team = await _teamRepository.FindAsync(form.TeamId);

            var data = _mapper.Map<OAPersonEntry>(form);

            data.TeamName = team?.Name ?? "";
            data.SysTenantId = LoginUser.TenantId.TryGuid().TryGuid();
            data.CreatorId = LoginUser.Id.TryGuid().TryGuid();
            data.CreatorName = LoginUser.DisplayName;

            var person = _mapper.Map<OAPerson>(form);
            person.ReInitExtendInfomation();
            data.ExtendInformationJson = person.ExtendInformationJson;
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAPersonEntryForm form)
        {
            var exists = await _repository.GetByNameAndMobileAsync(form.Name, form.MobilePhone);
            if (exists != null && exists.Id != form.Id)
                return BaseErrType.DataExist;

            var data = await _repository.FindAsync(form.Id);
            if (data == null) return BaseErrType.DataNotFound;

            _mapper.Map(form, data);

            var person = _mapper.Map<OAPerson>(form);
            person.ReInitExtendInfomation();
            data.ExtendInformationJson = person.ExtendInformationJson;

            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">项目id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            var data = await _repository.GetListByIdsAsync(ids);
            if (!data.Any()) 
                return BaseErrType.DataEmpty;
            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 确认到岗
        /// </summary>
        /// <param name="id">数据id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> ConfirmAsync(Guid id)
        {
            return await DeleteAsync(new List<Guid>() { id });
        }

        /// <summary>
        /// 填写资料
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdatePersonFileAsync(OAPersonalEntryFileForm form)
        {
            var data = await _repository.FindAsync(form.Id);
            if (data == null) return BaseErrType.DataNotFound;

            _mapper.Map(form, data);
            data.IsSubmitEntryFile = true;
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 由入职登记与到岗表单构建人员档案表单（含附加信息默认值规则）
        /// </summary>
        /// <param name="entry">入职登记</param>
        /// <param name="form">到岗表单</param>
        /// <returns>人员档案表单</returns>
        public Task<OAPersonForm> BuildPersonFormAsync(OAPersonEntryAggr entry, OAPersonEntryConfirmForm form)
        {
            var info = _mapper.Map<OAPersonEntryConfirmForm, OAPersonForm>(form);
            // 将入职资料的id传递给人员档案，直接将上传的附件同步而不需要转移文件
            info.Id = entry.Id;

            if (entry.IsSubmitEntryFile && !entry.ExtendInformationJson.IsNullOrWhiteSpace())
            {
                info.ExtendInformations = entry.ExtendInformationJson.FromJson<List<OAPersonExtenInformationFieldVo>>();
                info.IdCard = info.ExtendInformations == null ? "" : info.ExtendInformations.FirstOrDefault(w => w.Name == new OAPersonIdCardVo().Name)?.Value;
            }
            if (info.ExtendInformations == null)
                info.ExtendInformations = new List<OAPersonExtenInformationFieldVo>();

            var entryDate = DateTime.Now.ToString("yyyy-MM-dd");

            FillExtendInformation(info, new OAPersonWorkNumberVo().Name, info.WorkNumber, true);
            FillExtendInformation(info, new OAPersonEntryDateVo().Name, entryDate, true);
            FillExtendInformation(info, new OAPersonEmployeeStatusVo().Name, info.EmployeeStatus, true);
            FillExtendInformation(info, new OAPersonEmployeeTypeVo().Name, info.EmployeeType, true);
            FillExtendInformation(info, new OAPersonRemarkVo().Name, info.Remark, true);

            if (info.EmployeeStatus == OAPersonEmployeeStatusVo.ValueTrial)
            {
                // 试月：已填写的保留员工填写值，未填写的补默认 3 个月
                FillExtendInformation(info, new OAPersonTryDateVo().Name, "3个月", false);
                FillExtendInformation(info, new OAPersonPlanEntryDateVo().Name, entryDate.TryDateTime().AddMonths(3).ToString("yyyy-MM-dd"), true);
            }

            return Task.FromResult(info);
        }

        /// <summary>
        /// 写入或覆盖附加信息字段
        /// </summary>
        /// <param name="info">人员档案表单</param>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="overwrite">已存在时是否覆盖</param>
        private void FillExtendInformation(OAPersonForm info, string name, string value, bool overwrite)
        {
            var item = info.ExtendInformations.FirstOrDefault(w => w.Name == name);
            if (item == null)
            {
                info.ExtendInformations.Add(new OAPersonExtenInformationFieldVo() { Name = name, Value = value });
            }
            else if (overwrite)
            {
                item.Value = value;
            }
        }
    }
}
