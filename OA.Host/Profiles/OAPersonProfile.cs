using System;
using System.Text;
using System.Collections.Generic;
using AutoMapper;
using OA.Domain.Models;
using OA.Application.Dtos;
using OA.Domain.AggregateRoots;
using System.Linq;
using OneForAll.Core.Extension;
using OA.Domain.ValueObjects;
using OA.Domain.Enums;
using OA.Domain.ExcelModels;
using OA.Domain.Aggregates;

namespace OA.Host.Profiles
{
    public class OAPersonProfile : Profile
    {
        public OAPersonProfile()
        {
            CreateMap<OAPerson, OAPersonBasicInfoDto>();
            CreateMap<OAPerson, OAPersonDto>()
                .ForMember(t => t.ExtendInformations, a => a.MapFrom(e => e.ExtendInformationJson.FromJson<List<OAPersonExtenInformationFieldVo>>()));

            CreateMap<OAPerson, OATeamMemberDto>();

            CreateMap<OAPerson, OANoTeamPersonDto>();

            CreateMap<OAPerson, OAPersonExcel>()
                .ForMember(t => t.Sex, a => a.MapFrom(e => e.Sex ? "男" : "女"));

            CreateMap<OAPerson, OAPersonBirthdayCareDto>(); ;
            CreateMap<OAPerson, OAPersonCompanyCareDto>();

            CreateMap<OAPersonForm, OAPerson>();

            CreateMap<OATeamMemberImportForm, OAPerson>()
                .ForMember(t => t.EntryDate, a => a.MapFrom(e => e.EntryDate.IsNullOrEmpty() ? new DateTime?() : e.EntryDate.TryDateTime()))
                .ForMember(t => t.LeaveDate, a => a.MapFrom(e => e.EntryDate.IsNullOrEmpty() ? new DateTime?() : e.EntryDate.TryDateTime()));
        }
    }
}

