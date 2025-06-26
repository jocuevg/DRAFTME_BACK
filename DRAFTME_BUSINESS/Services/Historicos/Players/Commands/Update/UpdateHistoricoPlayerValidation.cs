using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Historicos.Players.Commands.Update;
public class UpdateHistoricoPlayerValidation : AbstractValidator<UpdateHistoricoPlayer>
{
    public UpdateHistoricoPlayerValidation(IRepository<Player> repository, IRepository<HistoricoTeam> repositoryHistorico)
    {
        RuleFor(x => x.PlayerId).MustAsync(async (id, cancellationToken) =>
            await repository.Query.AnyAsync(x => x.Id == id, cancellationToken)).WithMessage("No existe un jugador con ese identificador");
        RuleFor(x => x.HistoricoTeamId).MustAsync(async (id, cancellationToken) =>
            await repositoryHistorico.Query.AnyAsync(x => x.Id == id, cancellationToken)).WithMessage("No existe un historico de equipo con ese identificador");
        RuleFor(x => x.Goles).GreaterThanOrEqualTo(0).WithMessage("Goles inválidos");
        RuleFor(x => x.Asistencias).GreaterThanOrEqualTo(0).WithMessage("Asistencias inválidas");
        RuleFor(x => x.Minutos).GreaterThanOrEqualTo(0).WithMessage("Cantidad de minutos inválida");
        RuleFor(x => x.Temporada).MaximumLength(10).WithMessage("Maximo temporada de 10 caracteres");
    }
}
