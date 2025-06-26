using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Historicos.Teams.Commands.Update;
public class UpdateHistoricoTeamValidation : AbstractValidator<UpdateHistoricoTeam>
{
    public UpdateHistoricoTeamValidation(IRepository<Categoria> repository, IRepository<Team> repositoryTeam)
    {
        RuleFor(x => x.TeamId).MustAsync(async (id, cancellationToken) =>
            await repositoryTeam.Query.AnyAsync(x => x.Id == id, cancellationToken)).WithMessage("No existe una equipo con ese identificador");
        RuleFor(x => x.CategoriaId).MustAsync(async (id, cancellationToken) =>
            await repository.Query.AnyAsync(x => x.Id == id, cancellationToken)).WithMessage("No existe una categoria con ese identificador");
        RuleFor(x => x.Clasificacion).GreaterThanOrEqualTo(0).WithMessage("Clasificación inválida");
        RuleFor(x => x.Puntos).GreaterThanOrEqualTo(0).WithMessage("Cantidad de puntos inválida");
        RuleFor(x => x.Temporada).MaximumLength(10).WithMessage("Maximo temporada de 10 caracteres");
    }
}
