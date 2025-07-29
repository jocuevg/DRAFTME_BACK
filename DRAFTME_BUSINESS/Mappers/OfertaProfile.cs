using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Models;

namespace DRAFTME_BUSINESS.Mappers;
public class OfertaProfile : Profile
{
    public OfertaProfile()
    {
        CreateMap<Oferta, OfertaDTO>().ReverseMap();
    }
}
