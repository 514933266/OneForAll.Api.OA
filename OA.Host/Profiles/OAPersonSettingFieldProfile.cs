using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using AutoMapper;
using OA.Domain.Models;
using OA.Application.Dtos;
using OA.Domain.Aggregates;
using OA.Domain.AggregateRoots;
using OA.Domain.ValueObjects;
using OneForAll.Core.Extension;

namespace OA.Host.Profiles
{
    public class OAPersonSettingFieldProfile : Profile
    {
        public OAPersonSettingFieldProfile()
        {
            CreateMap<OAPersonSettingField, OAPersonSettingFieldDto>()
                .ForMember(t => t.TypeDetails, a => a.MapFrom(e => e.TypeDetail.FromJson<IEnumerable<OAPersonSettingFieldTypeDetailVo>>()));
            CreateMap<OAPersonSettingFieldForm, OAPersonSettingField>()
                .ForMember(t => t.TypeDetail, a => a.MapFrom(e => e.TypeDetails.ToJson()));

            CreateMap<OAPersonNameVo, OAPersonSettingField>();
            CreateMap<OAPersonEmailVo, OAPersonSettingField>();
            CreateMap<OAPersonTeamVo, OAPersonSettingField>();
            CreateMap<OAPersonTeamLeaderVo, OAPersonSettingField>();
            CreateMap<OAPersonJobVo, OAPersonSettingField>();
            CreateMap<OAPersonMobilePhoneVo, OAPersonSettingField>();
            CreateMap<OAPersonWorkNumberVo, OAPersonSettingField>();
            CreateMap<OAPersonRemarkVo, OAPersonSettingField>();
            CreateMap<OAPersonEntryDateVo, OAPersonSettingField>();
            CreateMap<OAPersonLeaveDateVo, OAPersonSettingField>();
            CreateMap<OAPersonJoinAgeVo, OAPersonSettingField>();
            CreateMap<OAPersonEmployeeTypeVo, OAPersonSettingField>();
            CreateMap<OAPersonEmployeeStatusVo, OAPersonSettingField>();
            CreateMap<OAPersonTryDateVo, OAPersonSettingField>();
            CreateMap<OAPersonActualEntryDateVo, OAPersonSettingField>();
            CreateMap<OAPersonPlanEntryDateVo, OAPersonSettingField>();
            CreateMap<OAPersonIdCardVo, OAPersonSettingField>();
            CreateMap<OAPersonIdCardAddress, OAPersonSettingField>();
            CreateMap<OAPersonBirthdayVo, OAPersonSettingField>();
            CreateMap<OAPersonAgeVo, OAPersonSettingField>();
            CreateMap<OAPersonSexVo, OAPersonSettingField>();
            CreateMap<OAPersonNationVo, OAPersonSettingField>();
            CreateMap<OAPersonIdCardValidDate, OAPersonSettingField>();
            CreateMap<OAPersonMaritalStatusVo, OAPersonSettingField>();
            CreateMap<OAPersonFirstWorkDateVo, OAPersonSettingField>();
            CreateMap<OAPersonWorkAgeVo, OAPersonSettingField>();
            CreateMap<OAPersonHouseholdTypeVo, OAPersonSettingField>();
            CreateMap<OAPersonPoliticsStatusVo, OAPersonSettingField>();
            CreateMap<OAPersonSocialSecurityAccountVo, OAPersonSettingField>();
            CreateMap<OAPersonProvidentFundAccountVo, OAPersonSettingField>();
            CreateMap<OAPersonHometownVo, OAPersonSettingField>();
            CreateMap<OAPersonEducationBackgroundVo, OAPersonSettingField>();
            CreateMap<OAPersonSchoolVo, OAPersonSettingField>();
            CreateMap<OAPersonSchoolDateVo, OAPersonSettingField>();
            CreateMap<OAPersonMajorVo, OAPersonSettingField>();
            CreateMap<OAPersonBankAccount, OAPersonSettingField>();
            CreateMap<OAPersonBankVo, OAPersonSettingField>();
            CreateMap<OAPersonBranchBankVo, OAPersonSettingField>();
            CreateMap<OAPersonContractCompanyVo, OAPersonSettingField>();
            CreateMap<OAPersonContractTypeVo, OAPersonSettingField>();
            CreateMap<OAPersonContractStartDateVo, OAPersonSettingField>();
            CreateMap<OAPersonContractEndDateVo, OAPersonSettingField>();
            CreateMap<OAPersonContractLimitVo, OAPersonSettingField>();
            CreateMap<OAPersonContractSignCountVo, OAPersonSettingField>();
            CreateMap<OAPersonContactNameVo, OAPersonSettingField>();
            CreateMap<OAPersonContactRelationshipVo, OAPersonSettingField>();
            CreateMap<OAPersonContactMobilePhoneVo, OAPersonSettingField>();
            CreateMap<OAPersonFamilyNameVo, OAPersonSettingField>();
            CreateMap<OAPersonFamilyRelationshipVo, OAPersonSettingField>();
            CreateMap<OAPersonFamilySexVo, OAPersonSettingField>();
            CreateMap<OAPersonFamilyBirthdayVo, OAPersonSettingField>();
            CreateMap<OAPersonFamilyMobilePhoneVo, OAPersonSettingField>();
            CreateMap<OAPersonIdCardFile, OAPersonSettingField>();
            CreateMap<OAPersonIdCardFile2, OAPersonSettingField>();
            CreateMap<OAPersonQualificationFileVo, OAPersonSettingField>();
            CreateMap<OAPersonDegreeFileVo, OAPersonSettingField>();
            CreateMap<OAPersonResignationFileVo, OAPersonSettingField>();
            CreateMap<OAPersonIconFileVo, OAPersonSettingField>();
        }
    }
}
