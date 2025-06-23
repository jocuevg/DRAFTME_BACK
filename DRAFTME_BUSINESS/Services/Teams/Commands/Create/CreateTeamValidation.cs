using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;


namespace DRAFTME_BUSINESS.Services.Teams.Commands.Create;
public class CreateTeamValidation : AbstractValidator<CreateTeam>
{
    public CreateTeamValidation(IRepository<Categoria> repository)
    {
        RuleFor(x => x.Nombre).MaximumLength(20).WithMessage("Maximo nombre de 20 caracteres");
        RuleFor(x => x.Formacion).InclusiveBetween(new DateTime(1857,1,1),DateTime.Today).WithMessage("Fecha de inaguración inválida");
        RuleFor(x => x.NumSocios).GreaterThanOrEqualTo(0).WithMessage("Número de socios inválido");
        RuleFor(x => x.Estadio).MaximumLength(50).WithMessage("Maximo estadio de 50 caracteres");
        RuleFor(x => x.CategoriaId).MustAsync(async (id, cancellationToken) =>
            await repository.Query.AnyAsync(x => x.Id == id, cancellationToken)).WithMessage("No existe una categoria con ese identificador");
        RuleFor(x => x.Clasificacion).GreaterThanOrEqualTo(0).WithMessage("Clasificación inválida");
        RuleFor(x => x.Puntos).GreaterThanOrEqualTo(0).WithMessage("Cantidad de puntos inválida");
    }
}
