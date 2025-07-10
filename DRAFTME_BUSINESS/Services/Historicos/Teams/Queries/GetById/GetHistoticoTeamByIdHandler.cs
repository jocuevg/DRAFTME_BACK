using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Historicos.Teams.Queries.GetById;
public class GetHistoticoTeamByIdHandler(IRepository<HistoricoTeam> repository, IMapper mapper) : IRequestHandler<GetHistoticoTeamById, HistoricoTeamDTO>
{
    public async Task<HistoricoTeamDTO> Handle(GetHistoticoTeamById request, CancellationToken cancellationToken)
    {
        var historico = await repository.Query
            .Include(x => x.Categoria)
            .Include(x => x.Team)
                .ThenInclude(x => x.Categoria)
            .Include(x => x.Plantilla)
                .ThenInclude(x=>x.Player)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (historico is null)
        {
            throw new KeyNotFoundException($"No se ha encontrado el historico de equipo con id: {request.Id}");
        }
        else
        {
            return mapper.Map<HistoricoTeamDTO>(historico);
        }
    }
}
