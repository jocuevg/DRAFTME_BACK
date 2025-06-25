using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Categorias.Queries.GetById;
public class GetCategoriaByIdHandler(IRepository<Categoria> repository, Mapper mapper) : IRequestHandler<GetCategoriaById, CategoriaDTO>
{
    public async Task<CategoriaDTO> Handle(GetCategoriaById request, CancellationToken cancellationToken)
    {
        var categoria = await repository.Query.Include(x=>x.Equipos)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (categoria is null)
        {
            throw new KeyNotFoundException($"No se ha encontrado la categoria con id: {request.Id}");
        }
        else
        {
            return mapper.Map<CategoriaDTO>(categoria);
        }
    }
}
