using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Models;

namespace DRAFTME_BUSINESS.Mappers;
public class PlayerProfile : Profile
{
    public PlayerProfile() {
        CreateMap<Player, PlayerDTO>().ReverseMap();
        CreateMap<Player, PlayerDTOSumarized>().ReverseMap()
            .ForMember(dest => dest.Biografia, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Historico, opt => opt.Ignore());
    }
}
