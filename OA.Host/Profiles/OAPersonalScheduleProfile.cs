using AutoMapper;
using OA.Application.Dtos;
using OA.Domain.AggregateRoots;
using OA.Domain.Enums;
using OA.Domain.Models;
using OA.Domain.ValueObjects;
using OneForAll.Core.Extension;
using System.Collections.Generic;

namespace OA.Host.Profiles
{
    public class OAPersonalScheduleProfile : Profile
    {
        public OAPersonalScheduleProfile()
        {
            CreateMap<OAPersonalSchedule, OAPersonalScheduleDto>()
                .ForMember(t => t.NotificationTypes, a => a.MapFrom(e => e.NotificationTypeJson.FromJson<List<OANotificationTypeEnum>>()));

            CreateMap<OAPersonalScheduleForm, OAPersonalSchedule>()
                .ForMember(t => t.NotificationTypeJson, a => a.MapFrom(e => e.NotificationTypes.ToJson()));
        }
    }
}
