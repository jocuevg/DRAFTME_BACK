using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Players.Queries.GetAll;
public class GetAllPlayersHandler(IRepository<Player> repository, IMapper mapper) : IRequestHandler<GetAllPlayers, List<PlayerDTOSumarized>>
{
    public async Task<List<PlayerDTOSumarized>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
    {
        return await repository.Query.Include(x => x.Team).ThenInclude(x=>x.Categoria)
            .Select(x => mapper.Map<PlayerDTOSumarized>(x)).ToListAsync(cancellationToken);
    }
}
