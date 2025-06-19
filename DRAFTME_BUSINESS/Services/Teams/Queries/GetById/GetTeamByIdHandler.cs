using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Teams.Queries.GetById;
public class GetTeamByIdHandler(IRepository<Team> repository, Mapper mapper) : IRequestHandler<GetTeamById, TeamDTO>
{
    public async Task<TeamDTO> Handle(GetTeamById request, CancellationToken cancellationToken)
    {
        var team = await repository.Query
            .Include(x => x.Categoria)
            .Include(x => x.Historico)
                .ThenInclude(x=> x.Categoria)
            .Include(x => x.Plantilla)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (team is null)
        {
            throw new KeyNotFoundException($"No se ha encontrado el equipo con id: {request.Id}");
        }
        else
        {
            return mapper.Map<TeamDTO>(team);
        }
    }
}
