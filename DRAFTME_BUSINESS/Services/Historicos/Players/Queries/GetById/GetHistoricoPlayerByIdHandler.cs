using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Historicos.Players.Queries.GetById;
public class GetHistoricoPlayerByIdHandler(IRepository<HistoricoPlayer> repository, Mapper mapper) : IRequestHandler<GetHistoricoPlayerById, HistoricoPlayerDTO>
{
    public async Task<HistoricoPlayerDTO> Handle(GetHistoricoPlayerById request, CancellationToken cancellationToken)
    {
        var historico = await repository.Query
                    .Include(x => x.Player).ThenInclude(x=>x.Team)
                    .Include(x => x.HistoricoTeam).ThenInclude(x => x.Categoria)
                    .Include(x => x.HistoricoTeam).ThenInclude(x => x.Team)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (historico is null)
        {
            throw new KeyNotFoundException($"No se ha encontrado el historico de jugador con id: {request.Id}");
        }
        else
        {
            return mapper.Map<HistoricoPlayerDTO>(historico);
        }
    }
}
