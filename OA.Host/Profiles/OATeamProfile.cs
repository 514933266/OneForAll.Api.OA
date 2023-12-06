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
    public class OATeamProfile : Profile
    {
        public OATeamProfile()
        {
            CreateMap<OATeam, OATeamTreeAggr>();
            CreateMap<OATeamTreeAggr, OATeamTreeDto>();
            CreateMap<OATeam, OATeamDto>();
            CreateMap<OATeamForm, OATeam>();
            CreateMap<OATeamSortForm, OATeam>();
        }
    }
}