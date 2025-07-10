using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Players.Queries.GetById;
public class GetPlayerByIdHandler(IRepository<Player> repository, IMapper mapper) : IRequestHandler<GetPlayerById, PlayerDTO>
{
    public async Task<PlayerDTO> Handle(GetPlayerById request, CancellationToken cancellationToken)
    {
        var player = await repository.Query
            .Include(x => x.User)
            .Include(x => x.Team)
                .ThenInclude(x => x.Categoria)
            .Include(x => x.Historico)
                .ThenInclude(x=>x.HistoricoTeam)
                    .ThenInclude(x=>x.Categoria)
            .Include(x => x.Historico)
                .ThenInclude(x => x.HistoricoTeam)
                    .ThenInclude(x => x.Team)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (player is null)
        {
            throw new KeyNotFoundException($"No se ha encontrado el jugador con id: {request.Id}");
        }
        else
        {
            return mapper.Map<PlayerDTO>(player);
        }
    }
}
