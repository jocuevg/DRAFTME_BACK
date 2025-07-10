using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Historicos.Teams.Commands.Update;
public class UpdateHistoricoTeamHandler(IRepository<HistoricoTeam> repository, IValidator<UpdateHistoricoTeam> validator, IMapper mapper) : IRequestHandler<UpdateHistoricoTeam, HistoricoTeamDTO>
{
    public async Task<HistoricoTeamDTO> Handle(UpdateHistoricoTeam request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }

        var historico = await repository.Query
            .Include(x => x.Categoria)
            .Include(x => x.Team)
                .ThenInclude(x => x.Categoria)
            .Include(x => x.Plantilla)
                .ThenInclude(x => x.Player)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        historico.TeamId = request.TeamId;
        historico.Temporada = request.Temporada;
        historico.CategoriaId = request.CategoriaId;
        historico.Clasificacion = request.Clasificacion;
        historico.Puntos = request.Puntos;

        await repository.SaveChangesAsync();
        return mapper.Map<HistoricoTeamDTO>(historico);

    }
}
