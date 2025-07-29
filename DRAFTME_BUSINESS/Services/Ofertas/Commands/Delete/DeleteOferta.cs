using MediatR;

namespace DRAFTME_BUSINESS.Services.Ofertas.Commands.Delete;
public class DeleteOferta : IRequest
{
    public int Id { get; set; }
}
