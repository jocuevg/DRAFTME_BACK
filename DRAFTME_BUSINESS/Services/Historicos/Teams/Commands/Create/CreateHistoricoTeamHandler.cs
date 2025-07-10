using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Historicos.Teams.Commands.Create;
public class CreateHistoricoTeamHandler(IRepository<HistoricoTeam> repository, IValidator<CreateHistoricoTeam> validator, IMapper mapper) : IRequestHandler<CreateHistoricoTeam, HistoricoTeamDTO>
{
    public async Task<HistoricoTeamDTO> Handle(CreateHistoricoTeam request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }

        var historico = new HistoricoTeam
        {
            TeamId = request.TeamId,
            Temporada = request.Temporada,
            CategoriaId = request.CategoriaId,
            Clasificacion = request.Clasificacion,
            Puntos = request.Puntos,
        };
        repository.Add(historico);
        await repository.SaveChangesAsync();
        return mapper.Map<HistoricoTeamDTO>(historico);
    }
}
