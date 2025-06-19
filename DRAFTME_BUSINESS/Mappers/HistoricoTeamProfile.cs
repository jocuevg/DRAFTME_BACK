using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Models;

namespace DRAFTME_BUSINESS.Mappers;
public class HistoricoTeamProfile : Profile
{
    public HistoricoTeamProfile() {
        CreateMap<HistoricoTeam, HistoricoTeamDTO>().ReverseMap();
        CreateMap<HistoricoTeam, HistoricoTeamDTOSumarized>().ReverseMap()
            .ForMember(dest => dest.Puntos, opt => opt.Ignore())
            .ForMember(dest => dest.Plantilla, opt => opt.Ignore());
    }
}
