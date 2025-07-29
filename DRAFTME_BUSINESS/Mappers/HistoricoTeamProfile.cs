using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Models;

namespace DRAFTME_BUSINESS.Mappers;
public class HistoricoTeamProfile : Profile
{
    public HistoricoTeamProfile() {
        CreateMap<HistoricoTeam, HistoricoTeamDTO>().ReverseMap();
    }
}
