using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Models;

namespace DRAFTME_BUSINESS.Mappers;
public class CategoriaProfile : Profile
{
    public CategoriaProfile() {
        CreateMap<Categoria, CategoriaDTOSumarized>().ReverseMap().ForMember(dest => dest.Equipos, opt => opt.Ignore());
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
    }
}
