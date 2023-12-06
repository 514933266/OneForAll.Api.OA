using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using AutoMapper;
using OA.Domain.Models;
using OA.Application.Dtos;
using OA.Domain.Aggregates;
using OA.Domain.AggregateRoots;

namespace OA.Host.Profiles
{
    public class OAJobProfile : Profile
    {
        public OAJobProfile()
        {
            CreateMap<OAJob, OAJobDto>()
                .ForMember(t => t.TypeId, a => a.MapFrom(e => e.OAJobTypeId))
                .ForMember(t => t.DutyId, a => a.MapFrom(e => e.OAJobDutyId))
                .ForMember(t => t.LevelId, a => a.MapFrom(e => e.OAJobLevelId))
                .ForMember(t => t.TeamId, a => a.MapFrom(e => e.OATeamId));
            CreateMap<OAJobAggr, OAJobDto>()
                .ForMember(t => t.TypeId, a => a.MapFrom(e => e.OAJobTypeId))
                .ForMember(t => t.DutyId, a => a.MapFrom(e => e.OAJobDutyId))
                .ForMember(t => t.LevelId, a => a.MapFrom(e => e.OAJobLevelId))
                .ForMember(t => t.TeamId, a => a.MapFrom(e => e.OATeamId));
            CreateMap<OAJobForm, OAJob>()
                .ForMember(t => t.OAJobTypeId, a => a.MapFrom(e => e.TypeId))
                .ForMember(t => t.OAJobDutyId, a => a.MapFrom(e => e.DutyId))
                .ForMember(t => t.OAJobLevelId, a => a.MapFrom(e => e.LevelId))
                .ForMember(t => t.OATeamId, a => a.MapFrom(e => e.TeamId));
        }
    }
}
