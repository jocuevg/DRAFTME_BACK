using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Historicos.Players.Queries.GetAllByPlayer;
public class GetAllHistoricoPlayerByPlayerHandler(IRepository<HistoricoPlayer> repository, IMapper mapper)
    : IRequestHandler<GetAllHistoricoPlayerByPlayer, List<HistoricoPlayerDTO>>
{
    public async Task<List<HistoricoPlayerDTO>> Handle(GetAllHistoricoPlayerByPlayer request, CancellationToken cancellationToken)
    {
        return await repository.Query
            .Include(x => x.Player)
            .Include(x => x.HistoricoTeam).ThenInclude(x => x.Team)
            .Include(x => x.HistoricoTeam).ThenInclude(x => x.Categoria)
            .Where(x => x.PlayerId == request.PlayerId)
            .Select(x => mapper.Map<HistoricoPlayerDTO>(x)).ToListAsync(cancellationToken);
    }
}
