using MediatR;

namespace DRAFTME_BUSINESS.Services.Categorias.Commands.Delete;
public class DeleteCategoria : IRequest
{
    public int Id { get; set; }
}
