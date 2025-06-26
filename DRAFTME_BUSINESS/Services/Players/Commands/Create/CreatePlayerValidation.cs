using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Players.Commands.Create;
public class CreatePlayerValidation : AbstractValidator<CreatePlayer>
{
    public CreatePlayerValidation(IRepository<User> repository, IRepository<Team> repositoryTeam)
    {
        RuleFor(x => x.Nombre).MaximumLength(20).WithMessage("Maximo nombre de 20 caracteres");
        RuleFor(x => x.Apellidos).MaximumLength(100).WithMessage("Maximo apellidos de 100 caracteres");
        RuleFor(x => x.Nacimiento).InclusiveBetween(new DateTime(1857, 1, 1), DateTime.Today).WithMessage("Fecha de nacimiento inválida");
        RuleFor(x => x.Posicion).MaximumLength(20).WithMessage("Maximo pasicion de 20 caracteres");
        RuleFor(x => x.Biografia).MaximumLength(500).WithMessage("Maxima bibliografia de 500 caracteres");

        RuleFor(x => x.UserId).MustAsync(async (username, cancellationToken) =>
            await repository.Query.AnyAsync(x => x.Username == username, cancellationToken)).WithMessage("Nombre de usuario no existe");
        RuleFor(x => x.TeamId).MustAsync(async (team, cancellationToken) =>
            await repositoryTeam.Query.AnyAsync(x => x.Id == team, cancellationToken)).WithMessage("Equipo no existe");

        RuleFor(x => x.Goles).GreaterThanOrEqualTo(0).WithMessage("Número de goles inválido");
        RuleFor(x => x.Asistencias).GreaterThanOrEqualTo(0).WithMessage("Número de asistencias inválido");
        RuleFor(x => x.Minutos).GreaterThanOrEqualTo(0).WithMessage("Número de minutos inválido");
    }
}
