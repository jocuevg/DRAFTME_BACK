using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Categorias.Queries.GetById;
public class GetCategoriaById : IRequest<CategoriaDTO>
{
    public int Id { get; set; }
}
