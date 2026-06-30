using AutoMapper;
using OA.Application.Dtos;
using OA.Domain.Aggregates;

namespace OA.Host.Profiles
{
    public class OAPersonContractProfile : Profile
    {
        public OAPersonContractProfile()
        {
            CreateMap<OATeamMemberAggr, OAPersonContractAggr>();
            CreateMap<OAPersonContractAggr, OAPersonContractDto>()
                .ForMember(t => t.TeamName, a => a.MapFrom(e => e.OATeam.Name));
        }
    }
}
