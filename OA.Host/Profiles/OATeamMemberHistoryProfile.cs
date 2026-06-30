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
    public class OATeamMemberHistoryProfile : Profile
    {
        public OATeamMemberHistoryProfile()
        {
            CreateMap<OATeamMemberHistory, OATeamMemberHistoryDto>();
        }
    }
}
