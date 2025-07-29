using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Models;

namespace DRAFTME_BUSINESS.Mappers;
public class TeamProfile : Profile
{
    public TeamProfile() {
        CreateMap<Team, TeamDTOSumarized>().ReverseMap()
            .ForMember(dest => dest.Formacion, opt => opt.Ignore())
            .ForMember(dest => dest.NumSocios, opt => opt.Ignore())
            .ForMember(dest => dest.Estadio, opt => opt.Ignore())
            .ForMember(dest => dest.Puntos, opt => opt.Ignore())
            .ForMember(dest => dest.Historico, opt => opt.Ignore())
            .ForMember(dest => dest.Ofertas, opt => opt.Ignore());
        CreateMap<Team, TeamDTO>().ReverseMap();
    }
}
