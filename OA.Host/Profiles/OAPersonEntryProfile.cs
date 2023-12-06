using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using AutoMapper;
using OA.Domain.Models;
using OA.Application.Dtos;
using OA.Domain.Aggregates;
using OA.Domain.AggregateRoots;
using OneForAll.Core.Extension;
using OA.Domain.ValueObjects;

namespace OA.Host.Profiles
{
    public class OAPersonEntryProfile : Profile
    {
        public OAPersonEntryProfile()
        {
            CreateMap<OAPersonEntry, OAPersonEntryDto>();
            CreateMap<OAPersonEntryAggr, OAPersonEntryDto>()
                .ForMember(t => t.TeamId, a => a.MapFrom(e => e.OATeam.Id))
                .ForMember(t => t.TeamName, a => a.MapFrom(e => e.OATeam.Name));
            CreateMap<OAPersonEntry, OAPersonalEntryFileDto>()
                .ForMember(t => t.ExtendInformations, a => a.MapFrom(e => e.ExtendInformationJson.FromJson<List<OAPersonExtenInformationFieldVo>>()));

            CreateMap<OAPersonEntryForm, OAPersonEntry>();
            CreateMap<OAPersonEntryForm, OAPerson>();
            CreateMap<OAPersonEntryConfirmForm, OAPersonForm>();
            CreateMap<OAPersonalEntryFileForm, OAPersonEntry>()
                .ForMember(t => t.ExtendInformationJson, a => a.MapFrom(e => e.ExtendInformations.ToJson()));
        }
    }
}
