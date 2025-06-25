using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Categorias.Queries.GetAll;
public class GetAllCategoriasHandler(IRepository<Categoria> repository, Mapper mapper) : IRequestHandler<GetAllCategorias, List<CategoriaDTOSumarized>>
{
    public async Task<List<CategoriaDTOSumarized>> Handle(GetAllCategorias request, CancellationToken cancellationToken)
    {
        return await repository.Query.Select(x => mapper.Map<CategoriaDTOSumarized>(x)).ToListAsync(cancellationToken);
    }
}
