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

namespace OA.Host.Profiles
{
    public class OAPersonSettingProfile : Profile
    {
        public OAPersonSettingProfile()
        {
            CreateMap<OAPersonSetting, OAPersonSettingDto>();
            CreateMap<OAPersonSetting, OAPersonSettingAggr>();
            CreateMap<OAPersonSettingAggr, OAPersonSettingDto>()
               .ForMember(t => t.Fields, a => a.MapFrom(e => e.OAPersonSettingFields));

            CreateMap<OAPersonSettingForm, OAPersonSetting>();
        }
    }
}
