using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Ofertas.Commands.Update;
public class UpdateOferta : IRequest<OfertaDTO>
{
    public int Id { get; set; }
    public string Posicion { get; set; }
    public string Descripcion { get; set; }
    public int TeamId { get; set; }
}

public class UpdateOfertaRequest
{
    public string Posicion { get; set; }
    public string Descripcion { get; set; }
    public int TeamId { get; set; }
}
