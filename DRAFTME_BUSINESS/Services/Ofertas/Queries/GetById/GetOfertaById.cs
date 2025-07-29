using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Ofertas.Queries.GetById;
public class GetOfertaById : IRequest<OfertaDTO>
{
    public int Id { get; set; }
}
