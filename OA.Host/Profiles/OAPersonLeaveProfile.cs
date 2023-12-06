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
    public class OAPersonLeaveProfile : Profile
    {
        public OAPersonLeaveProfile()
        {
            CreateMap<OAPersonLeave, OAPersonLeaveDto>();
            CreateMap<OAPersonLeaveAggr, OAPersonLeaveDto>()
                .ForMember(t => t.PersonId, a => a.MapFrom(e => e.OAPerson.Id))
                .ForMember(t => t.PersonName, a => a.MapFrom(e => e.OAPerson.Name))
                .ForMember(t => t.PersonJob, a => a.MapFrom(e => e.OAPerson.Job))
                .ForMember(t => t.EmployeeType, a => a.MapFrom(e => e.OAPerson.EmployeeType))
                .ForMember(t => t.TeamName, a => a.MapFrom(e => (e.OATeam == null ? "" : e.OATeam.Name)));
            CreateMap<OAPersonLeaveForm, OAPersonLeave>()
                .ForMember(t => t.OAPersonId, a => a.MapFrom(e => e.PersonId));
        }
    }
}
