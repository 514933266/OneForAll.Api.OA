using AutoMapper;
using OA.Application.Dtos;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Host.Profiles
{
    public class OATeamPersonContactProfile : Profile
    {
        public OATeamPersonContactProfile()
        {
            CreateMap<OATeamMemberAggr, OATeamMemberDto>()
                .ForMember(t => t.TeamId, a => a.MapFrom(e => e.OATeam.Id))
                .ForMember(t => t.IsLeave, a => a.MapFrom(e => (e.LeaveDate == null ? false : true)))
                .ForMember(t => t.IsLeader, a => a.MapFrom(e => e.Contact.IsLeader))
                .ForMember(t => t.CreateTime, a => a.MapFrom(e => e.Contact.CreateTime));
            CreateMap<OATeamPersonContact, OATeamMemberHistory>();
            CreateMap<OAPersonTeamInfoAggr, OAPersonTeamInfoDto>();
            CreateMap<OATeamMemberAggr, OAPersonTeamInfoAggr>();
            CreateMap<OATeamMemberAggr, OAPersonBirthdayCareDto>();
            CreateMap<OATeamMemberAggr, OAPersonCompanyCareDto>();

            CreateMap<OATeamMemberForm, OATeamPersonContact>()
                .ForMember(t => t.OATeamId, a => a.MapFrom(e => e.TeamId))
                .ForMember(t => t.OAPersonId, a => a.MapFrom(e => e.Id))
                .ForMember(t => t.IsLeader, a => a.MapFrom(e => e.IsLeader));
            CreateMap<OATeamMemberForm, OAPersonForm>();
            CreateMap<OATeamMemberForm, OAPerson>();
        }
    }
}
