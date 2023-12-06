using AutoMapper;
using OA.Application.Dtos;
using OA.Domain.Aggregates;

namespace OA.Host.Profiles
{
    public class OAPersonFormalProfile : Profile
    {
        public OAPersonFormalProfile()
        {
            CreateMap<OATeamMemberAggr, OAPersonFormalAggr>();
            CreateMap<OAPersonFormalAggr, OAPersonFormalDto>()
                .ForMember(t => t.TeamName, a => a.MapFrom(e => e.OATeam.Name));
        }
    }
}
