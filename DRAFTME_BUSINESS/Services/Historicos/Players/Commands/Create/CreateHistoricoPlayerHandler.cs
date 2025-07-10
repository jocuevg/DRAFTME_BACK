using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Historicos.Players.Commands.Create;
public class CreateHistoricoPlayerHandler(IRepository<HistoricoPlayer> repository, IValidator<CreateHistoricoPlayer> validator, IMapper mapper) : IRequestHandler<CreateHistoricoPlayer, HistoricoPlayerDTO>
{
    public async Task<HistoricoPlayerDTO> Handle(CreateHistoricoPlayer request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }

        var historico = new HistoricoPlayer
        {
            PlayerId = request.PlayerId,
            Temporada = request.Temporada,
            Goles = request.Goles,
            Asistencias = request.Asistencias,
            Minutos = request.Minutos,
            HistoricoTeamId = request.HistoricoTeamId,
        };
        repository.Add(historico);
        await repository.SaveChangesAsync();
        return mapper.Map<HistoricoPlayerDTO>(historico);
    }
}
