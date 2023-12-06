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
    public class OAJobLevelProfile : Profile
    {
        public OAJobLevelProfile()
        {
            CreateMap<OAJobLevel, OAJobLevelDto>();
            CreateMap<OAJobLevelForm, OAJobLevel>();
        }
    }
}
