using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using OneForAll.Core;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;
using System.Linq;
using OA.Domain.Enums;
using OA.Domain.ValueObjects;
using OneForAll.Core.Extension;
using OneForAll.EFCore;
using OA.Domain.Aggregates;

namespace OA.Domain
{
    /// <summary>
    /// 人员档案设置
    /// </summary>
    public class OAPersonSettingManager : OABaseManager, IOAPersonSettingManager
    {
        private readonly IOAPersonSettingRepository _repository;
        private readonly IOAPersonSettingFieldRepository _fieldRepository;
        public OAPersonSettingManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOAPersonSettingRepository repository,
            IOAPersonSettingFieldRepository fieldRepository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
            _fieldRepository = fieldRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonSettingAggr>> GetListAsync()
        {
            var data = await _repository.GetListAsync();

            if (!data.Any())
            {
                await CreateDefaultAsync();
                data = await _repository.GetListAsync();
            }

            var ids = data.Select(s => s.Id).ToList();
            var fields = await _fieldRepository.GetListBySettingAsync(ids);

            var result = _mapper.Map<IEnumerable<OAPersonSettingAggr>>(data);
            result.ForEach(e =>
            {
                e.OAPersonSettingFields = fields.Where(w => w.OAPersonSettingId == e.Id).OrderBy(o => o.SortNumber).ToList();
            });
            return result.OrderBy(o => o.SortNumber).ToList();
        }

        /// <summary>
        /// 生成默认配置
        /// </summary>
        /// <returns></returns>
        private async Task<BaseErrType> CreateDefaultAsync()
        {
            using (var tran = new UnitOfWork().BeginTransaction())
            {
                #region 基础信息
                var basicInfo = new OAPersonSetting() { SysTenantId = LoginUser.SysTenantId };
                basicInfo.Init(OAPersonSettingTypeEnum.BasicInformation);

                _repository.Add(basicInfo, tran);

                var b_item1 = _mapper.Map<OAPersonSettingField>(new OAPersonNameVo());
                var b_item2 = _mapper.Map<OAPersonSettingField>(new OAPersonEmailVo());
                var b_item3 = _mapper.Map<OAPersonSettingField>(new OAPersonTeamVo());
                var b_item4 = _mapper.Map<OAPersonSettingField>(new OAPersonTeamLeaderVo());
                var b_item5 = _mapper.Map<OAPersonSettingField>(new OAPersonJobVo());
                var b_item6 = _mapper.Map<OAPersonSettingField>(new OAPersonMobilePhoneVo());
                var b_item7 = _mapper.Map<OAPersonSettingField>(new OAPersonWorkNumberVo());
                var b_item8 = _mapper.Map<OAPersonSettingField>(new OAPersonJoinAgeVo());
                var b_item9 = _mapper.Map<OAPersonSettingField>(new OAPersonRemarkVo());

                b_item1.OAPersonSettingId = basicInfo.Id;
                b_item2.OAPersonSettingId = basicInfo.Id;
                b_item3.OAPersonSettingId = basicInfo.Id;
                b_item4.OAPersonSettingId = basicInfo.Id;
                b_item5.OAPersonSettingId = basicInfo.Id;
                b_item6.OAPersonSettingId = basicInfo.Id;
                b_item7.OAPersonSettingId = basicInfo.Id;
                b_item8.OAPersonSettingId = basicInfo.Id;
                b_item9.OAPersonSettingId = basicInfo.Id;

                _fieldRepository.AddRange(new List<OAPersonSettingField>() {
                    b_item1, b_item2, b_item3, b_item4,
                    b_item5,b_item6,b_item7,b_item8,b_item9
                }, tran);

                #endregion

                #region 工作信息
                var workInfo = new OAPersonSetting() { SysTenantId = LoginUser.SysTenantId };
                workInfo.Init(OAPersonSettingTypeEnum.WorkInformation);

                _repository.Add(workInfo, tran);

                var w_item1 = _mapper.Map<OAPersonSettingField>(new OAPersonEmployeeTypeVo());
                var w_item2 = _mapper.Map<OAPersonSettingField>(new OAPersonEmployeeStatusVo());
                var w_item3 = _mapper.Map<OAPersonSettingField>(new OAPersonTryDateVo());
                var w_item4 = _mapper.Map<OAPersonSettingField>(new OAPersonPlanEntryDateVo());
                var w_item5 = _mapper.Map<OAPersonSettingField>(new OAPersonActualEntryDateVo());
                var w_item6 = _mapper.Map<OAPersonSettingField>(new OAPersonEntryDateVo());
                var w_item7 = _mapper.Map<OAPersonSettingField>(new OAPersonLeaveDateVo());
                var w_item8 = _mapper.Map<OAPersonSettingField>(new OAPersonFirstWorkDateVo());
                var w_item9 = _mapper.Map<OAPersonSettingField>(new OAPersonWorkAgeVo());

                w_item1.OAPersonSettingId = workInfo.Id;
                w_item2.OAPersonSettingId = workInfo.Id;
                w_item3.OAPersonSettingId = workInfo.Id;
                w_item4.OAPersonSettingId = workInfo.Id;
                w_item5.OAPersonSettingId = workInfo.Id;
                w_item6.OAPersonSettingId = workInfo.Id;
                w_item7.OAPersonSettingId = workInfo.Id;
                w_item8.OAPersonSettingId = workInfo.Id;
                w_item9.OAPersonSettingId = workInfo.Id;

                _fieldRepository.AddRange(new List<OAPersonSettingField>() {
                    w_item1, w_item2, w_item3, w_item4,
                    w_item5,w_item6,w_item7,w_item8,w_item9
                }, tran);
                #endregion

                #region 个人信息
                var personalInfo = new OAPersonSetting() { SysTenantId = LoginUser.SysTenantId };
                personalInfo.Init(OAPersonSettingTypeEnum.PersonalInformation);

                _repository.Add(personalInfo, tran);

                var p_item1 = _mapper.Map<OAPersonSettingField>(new OAPersonIdCardVo());
                var p_item2 = _mapper.Map<OAPersonSettingField>(new OAPersonIdCardAddress());
                var p_item3 = _mapper.Map<OAPersonSettingField>(new OAPersonIdCardValidDate());
                var p_item4 = _mapper.Map<OAPersonSettingField>(new OAPersonBirthdayVo());
                var p_item5 = _mapper.Map<OAPersonSettingField>(new OAPersonAgeVo());
                var p_item6 = _mapper.Map<OAPersonSettingField>(new OAPersonSexVo());
                var p_item7 = _mapper.Map<OAPersonSettingField>(new OAPersonNationVo());
                var p_item8 = _mapper.Map<OAPersonSettingField>(new OAPersonMaritalStatusVo());
                var p_item9 = _mapper.Map<OAPersonSettingField>(new OAPersonPoliticsStatusVo());
                var p_item10 = _mapper.Map<OAPersonSettingField>(new OAPersonHouseholdTypeVo());
                var p_item11 = _mapper.Map<OAPersonSettingField>(new OAPersonHometownVo());
                var p_item12 = _mapper.Map<OAPersonSettingField>(new OAPersonSocialSecurityAccountVo());
                var p_item13 = _mapper.Map<OAPersonSettingField>(new OAPersonProvidentFundAccountVo());

                p_item1.OAPersonSettingId = personalInfo.Id;
                p_item2.OAPersonSettingId = personalInfo.Id;
                p_item3.OAPersonSettingId = personalInfo.Id;
                p_item4.OAPersonSettingId = personalInfo.Id;
                p_item5.OAPersonSettingId = personalInfo.Id;
                p_item6.OAPersonSettingId = personalInfo.Id;
                p_item7.OAPersonSettingId = personalInfo.Id;
                p_item8.OAPersonSettingId = personalInfo.Id;
                p_item9.OAPersonSettingId = personalInfo.Id;
                p_item10.OAPersonSettingId = personalInfo.Id;
                p_item11.OAPersonSettingId = personalInfo.Id;
                p_item12.OAPersonSettingId = personalInfo.Id;
                p_item13.OAPersonSettingId = personalInfo.Id;

                _fieldRepository.AddRange(new List<OAPersonSettingField>() {
                    p_item1, p_item2, p_item3, p_item4,
                    p_item5,p_item6,p_item7,p_item8,p_item9,p_item10,p_item11,p_item12,p_item13
                }, tran);
                #endregion

                #region 学历信息
                var eduInfo = new OAPersonSetting() { SysTenantId = LoginUser.SysTenantId };
                eduInfo.Init(OAPersonSettingTypeEnum.EducationInformation);

                _repository.Add(eduInfo, tran);

                var e_item1 = _mapper.Map<OAPersonSettingField>(new OAPersonEducationBackgroundVo());
                var e_item2 = _mapper.Map<OAPersonSettingField>(new OAPersonSchoolVo());
                var e_item3 = _mapper.Map<OAPersonSettingField>(new OAPersonSchoolDateVo());
                var e_item4 = _mapper.Map<OAPersonSettingField>(new OAPersonMajorVo());

                e_item1.OAPersonSettingId = eduInfo.Id;
                e_item2.OAPersonSettingId = eduInfo.Id;
                e_item3.OAPersonSettingId = eduInfo.Id;
                e_item4.OAPersonSettingId = eduInfo.Id;

                _fieldRepository.AddRange(new List<OAPersonSettingField>() {
                    e_item1, e_item2, e_item3, e_item4
                }, tran);
                #endregion

                #region 银行卡信息
                var bankCardInfo = new OAPersonSetting() { SysTenantId = LoginUser.SysTenantId };
                bankCardInfo.Init(OAPersonSettingTypeEnum.BankCardInformation);

                _repository.Add(bankCardInfo, tran);

                var bc_item1 = _mapper.Map<OAPersonSettingField>(new OAPersonBankAccount());
                var bc_item2 = _mapper.Map<OAPersonSettingField>(new OAPersonBankVo());
                var bc_item3 = _mapper.Map<OAPersonSettingField>(new OAPersonBranchBankVo());

                bc_item1.OAPersonSettingId = bankCardInfo.Id;
                bc_item2.OAPersonSettingId = bankCardInfo.Id;
                bc_item3.OAPersonSettingId = bankCardInfo.Id;

                _fieldRepository.AddRange(new List<OAPersonSettingField>() {
                    bc_item1, bc_item2, bc_item3
                }, tran);
                #endregion

                #region  合同信息
                var contractInfo = new OAPersonSetting() { SysTenantId = LoginUser.SysTenantId };
                contractInfo.Init(OAPersonSettingTypeEnum.ContractInformation);

                _repository.Add(contractInfo, tran);

                var c_item1 = _mapper.Map<OAPersonSettingField>(new OAPersonContractCompanyVo());
                var c_item2 = _mapper.Map<OAPersonSettingField>(new OAPersonContractTypeVo());
                var c_item3 = _mapper.Map<OAPersonSettingField>(new OAPersonContractStartDateVo());
                var c_item4 = _mapper.Map<OAPersonSettingField>(new OAPersonContractEndDateVo());
                var c_item5 = _mapper.Map<OAPersonSettingField>(new OAPersonContractLimitVo());
                var c_item6 = _mapper.Map<OAPersonSettingField>(new OAPersonContractSignCountVo());

                c_item1.OAPersonSettingId = contractInfo.Id;
                c_item2.OAPersonSettingId = contractInfo.Id;
                c_item3.OAPersonSettingId = contractInfo.Id;
                c_item4.OAPersonSettingId = contractInfo.Id;
                c_item5.OAPersonSettingId = contractInfo.Id;
                c_item6.OAPersonSettingId = contractInfo.Id;

                _fieldRepository.AddRange(new List<OAPersonSettingField>() {
                    c_item1, c_item2, c_item3,c_item4,c_item5,c_item6
                }, tran);
                #endregion

                #region 紧急联系人
                var contactInfo = new OAPersonSetting() { SysTenantId = LoginUser.SysTenantId };
                contactInfo.Init(OAPersonSettingTypeEnum.EmergencyContact);

                _repository.Add(contactInfo, tran);

                var ct_item1 = _mapper.Map<OAPersonSettingField>(new OAPersonContactNameVo());
                var ct_item2 = _mapper.Map<OAPersonSettingField>(new OAPersonContactRelationshipVo());
                var ct_item3 = _mapper.Map<OAPersonSettingField>(new OAPersonContactMobilePhoneVo());

                ct_item1.OAPersonSettingId = contactInfo.Id;
                ct_item2.OAPersonSettingId = contactInfo.Id;
                ct_item3.OAPersonSettingId = contactInfo.Id;

                _fieldRepository.AddRange(new List<OAPersonSettingField>() {
                    ct_item1, ct_item2, ct_item3
                }, tran);
                #endregion

                #region 家庭信息
                var familyInfo = new OAPersonSetting() { SysTenantId = LoginUser.SysTenantId };
                familyInfo.Init(OAPersonSettingTypeEnum.FamilyMember);

                _repository.Add(familyInfo, tran);

                var f_item1 = _mapper.Map<OAPersonSettingField>(new OAPersonFamilyNameVo());
                var f_item2 = _mapper.Map<OAPersonSettingField>(new OAPersonFamilyRelationshipVo());
                var f_item3 = _mapper.Map<OAPersonSettingField>(new OAPersonFamilySexVo());
                var f_item4 = _mapper.Map<OAPersonSettingField>(new OAPersonFamilyBirthdayVo());
                var f_item5 = _mapper.Map<OAPersonSettingField>(new OAPersonFamilyMobilePhoneVo());

                f_item1.OAPersonSettingId = familyInfo.Id;
                f_item2.OAPersonSettingId = familyInfo.Id;
                f_item3.OAPersonSettingId = familyInfo.Id;
                f_item4.OAPersonSettingId = familyInfo.Id;
                f_item5.OAPersonSettingId = familyInfo.Id;

                _fieldRepository.AddRange(new List<OAPersonSettingField>() {
                    f_item1, f_item2, f_item3,f_item4, f_item5
                }, tran);
                #endregion

                #region 个人材料
                var personalData = new OAPersonSetting() { SysTenantId = LoginUser.SysTenantId };
                personalData.Init(OAPersonSettingTypeEnum.PersonalData);

                _repository.Add(personalData, tran);

                var pa_item1 = _mapper.Map<OAPersonSettingField>(new OAPersonIdCardFile());
                var pa_item2 = _mapper.Map<OAPersonSettingField>(new OAPersonIdCardFile2());
                var pa_item3 = _mapper.Map<OAPersonSettingField>(new OAPersonQualificationFileVo());
                var pa_item4 = _mapper.Map<OAPersonSettingField>(new OAPersonDegreeFileVo());
                var pa_item5 = _mapper.Map<OAPersonSettingField>(new OAPersonResignationFileVo());
                var pa_item6 = _mapper.Map<OAPersonSettingField>(new OAPersonIconFileVo());

                pa_item1.OAPersonSettingId = personalData.Id;
                pa_item2.OAPersonSettingId = personalData.Id;
                pa_item3.OAPersonSettingId = personalData.Id;
                pa_item4.OAPersonSettingId = personalData.Id;
                pa_item5.OAPersonSettingId = personalData.Id;
                pa_item6.OAPersonSettingId = personalData.Id;

                _fieldRepository.AddRange(new List<OAPersonSettingField>() {
                    pa_item1,pa_item2,pa_item3,pa_item4,pa_item5,pa_item6
                }, tran);
                #endregion

                return await ResultAsync(tran.CommitAsync);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAPersonSettingForm entity)
        {
            var exists = await _repository.GetAsync(w => w.Text == entity.Text);
            if (exists != null) return BaseErrType.DataExist;
            var count = await _repository.CountAsync();

            var data = _mapper.Map<OAPersonSetting>(entity);
            data.SysTenantId = LoginUser.SysTenantId;
            data.IsShowGrouped = true;
            data.IsEditable = true;
            data.IsDefault = false;
            data.IsEnabled = true;
            data.Type = OAPersonSettingTypeEnum.None;
            data.SortNumber = count + 1;

            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAPersonSettingForm entity)
        {
            var exists = await _repository.GetAsync(w => w.Text == entity.Text);
            if (exists != null && exists.Id != entity.Id) return BaseErrType.DataExist;

            var data = await _repository.FindAsync(entity.Id);
            if (data == null) return BaseErrType.DataNotFound;
            if (!data.IsEditable) return BaseErrType.NotAllow;
            if (!data.IsShowGrouped && data.IsGrouped) return BaseErrType.DataError;

            data.Text = entity.Text;
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid id)
        {
            var effected = 0;
            var totalData = await _repository.GetListAsync();
            var data = totalData.FirstOrDefault(w => w.Id == id);
            if (data == null) return BaseErrType.DataNotFound;

            // 重新排序
            var index = 0;
            var updateData = totalData.Where(w => w.Id != id).OrderBy(o => o.SortNumber).ToList();
            updateData.ForEach(e =>
            {
                e.SortNumber = index;
                index++;
            });

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                updateData.ForEach(e =>
                {
                    _repository.Update(e, tran);
                });
                _repository.Delete(data, tran);
                effected = tran.Commit();
            }
            return Result(() => effected);
        }

        /// <summary>
        /// 重新排序
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> SortAsync(IEnumerable<Guid> ids)
        {
            var data = await _repository.GetListAsync(ids);
            var sortableData = data.Where(w => w.IsSortable);
            if (!sortableData.Any()) return BaseErrType.DataNotFound;

            // 重新排序
            var index = data.Count() - sortableData.Count();
            sortableData.ForEach(e =>
            {
                e.SortNumber = index;
                index++;
            });
            return await ResultAsync(() => _repository.SaveChangesAsync());
        }
    }
}
