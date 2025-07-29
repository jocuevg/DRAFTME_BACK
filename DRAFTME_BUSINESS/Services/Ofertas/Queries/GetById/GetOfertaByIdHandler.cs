using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Ofertas.Queries.GetById;
public class GetOfertaByIdHandler(IRepository<Oferta> repository, IMapper mapper) : IRequestHandler<GetOfertaById, OfertaDTO>
{
    public async Task<OfertaDTO> Handle(GetOfertaById request, CancellationToken cancellationToken)
    {
        var oferta = await repository.Query
            .Include(x => x.Team)
                .ThenInclude(x => x.Categoria)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (oferta is null)
        {
            throw new KeyNotFoundException($"No se ha encontrado la oferta con id: {request.Id}");
        }
        else
        {
            return mapper.Map<OfertaDTO>(oferta);
        }
    }
}
