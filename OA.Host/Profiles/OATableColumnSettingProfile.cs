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

namespace OA.Host.Profiles
{
    public class OATableColumnSettingProfile : Profile
    {
        public OATableColumnSettingProfile()
        {
            CreateMap<OATableColumnSetting, OATableColumnSettingDto>()
                .ForMember(t => t.VisiableFields, a => a.MapFrom(e => e.VisiableFields.FromJson<IEnumerable<string>>()));
            CreateMap<OATableColumnSettingForm, OATableColumnSetting>()
                .ForMember(t => t.VisiableFields, a => a.MapFrom(e => e.VisiableFields.ToJson()));
        }
    }
}
