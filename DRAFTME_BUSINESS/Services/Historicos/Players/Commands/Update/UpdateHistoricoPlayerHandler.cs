using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Historicos.Players.Commands.Update;
public class UpdateHistoricoPlayerHandler(IRepository<HistoricoPlayer> repository, IValidator<UpdateHistoricoPlayer> validator, Mapper mapper) : IRequestHandler<UpdateHistoricoPlayer, HistoricoPlayerDTO>
{
    public async Task<HistoricoPlayerDTO> Handle(UpdateHistoricoPlayer request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }

        var historico = await repository.Query
                    .Include(x => x.Player).ThenInclude(x => x.Team).ThenInclude(x=>x.Categoria)
                    .Include(x => x.HistoricoTeam).ThenInclude(x => x.Categoria)
                    .Include(x => x.HistoricoTeam).ThenInclude(x => x.Team).ThenInclude(x=>x.Categoria)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        historico.PlayerId = request.PlayerId;
        historico.Temporada = request.Temporada;
        historico.Goles = request.Goles;
        historico.Asistencias = request.Asistencias;
        historico.Minutos = request.Minutos;
        historico.HistoricoTeamId = request.HistoricoTeamId;

        await repository.SaveChangesAsync();
        return mapper.Map<HistoricoPlayerDTO>(historico);
    }
}
