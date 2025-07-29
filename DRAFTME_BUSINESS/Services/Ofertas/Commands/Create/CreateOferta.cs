using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Ofertas.Commands.Create;
public class CreateOferta : IRequest<OfertaDTO>
{
    public int TeamId { get; set; }
    public string Posicion { get; set; }
    public string Descripcion { get; set; }
}
