using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Ofertas.Queries.GetAll;
public class GetAllOfertasHandler(IRepository<Oferta> repository, IMapper mapper) : IRequestHandler<GetAllOfertas, List<OfertaDTO>>
{
    public async Task<List<OfertaDTO>> Handle(GetAllOfertas request, CancellationToken cancellationToken)
    {
        return await repository.Query.Include(x => x.Team).ThenInclude(x => x.Categoria)
            .Select(x => mapper.Map<OfertaDTO>(x)).ToListAsync(cancellationToken);
    }
}
