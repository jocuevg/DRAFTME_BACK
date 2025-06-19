using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Models;

namespace DRAFTME_BUSINESS.Mappers;
public class ScouterProfile : Profile
{
    public ScouterProfile() {
        CreateMap<Scouter,ScouterDTO>().ReverseMap();
    }
}
