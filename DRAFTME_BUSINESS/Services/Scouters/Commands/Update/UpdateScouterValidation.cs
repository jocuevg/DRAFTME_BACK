using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Scouters.Commands.Update;
public class UpdateScouterValidation : AbstractValidator<UpdateScouter>
{
    public UpdateScouterValidation(IRepository<User> repository, IRepository<Team> repositoryTeam)
    {
        RuleFor(x => x.Nombre).MaximumLength(20).WithMessage("Maximo nombre de 20 caracteres");
        RuleFor(x => x.Apellidos).MaximumLength(100).WithMessage("Maximo apellidos de 100 caracteres");
        RuleFor(x => x.Biografia).MaximumLength(500).WithMessage("Maxima bibliografia de 500 caracteres");
        RuleFor(x => x.Nacimiento).InclusiveBetween(new DateTime(1857, 1, 1), DateTime.Today).WithMessage("Fecha de nacimiento inválida");
        RuleFor(x => x.UserId).MaximumLength(20).WithMessage("Maximo nombre de usuario de 20 caracteres");
        RuleFor(x => x.UserId).MustAsync(async (username, cancellationToken) =>
            await repository.Query.AllAsync(x => x.Username != username, cancellationToken)).WithMessage("Nombre de usuario ocupado");
        RuleFor(x => x.TeamId).MustAsync(async (team, cancellationToken) =>
            await repositoryTeam.Query.AnyAsync(x => x.Id == team, cancellationToken)).WithMessage("Equipo no existe").When(x => x.TeamId.HasValue);
    }
}
