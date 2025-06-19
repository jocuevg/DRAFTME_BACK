using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Models;

namespace DRAFTME_BUSINESS.Mappers;
public class HistoricoPlayerProfile : Profile
{
    public HistoricoPlayerProfile() {
        CreateMap<HistoricoPlayer,HistoricoPlayerDTO>().ReverseMap();
    }
}
