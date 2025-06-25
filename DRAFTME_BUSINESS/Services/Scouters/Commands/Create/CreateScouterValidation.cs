using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Scouters.Commands.Create;
public class CreateScouterValidation : AbstractValidator<CreateScouter>
{
    public CreateScouterValidation(IRepository<User> repository)
    {
        RuleFor(x => x.Nombre).MaximumLength(20).WithMessage("Maximo nombre de 20 caracteres");
        RuleFor(x => x.Apellidos).MaximumLength(100).WithMessage("Maximo apellidos de 100 caracteres");
        RuleFor(x => x.Biografia).MaximumLength(500).WithMessage("Maxima bibliografia de 500 caracteres");
        RuleFor(x => x.Nacimiento).InclusiveBetween(new DateTime(1857, 1, 1), DateTime.Today).WithMessage("Fecha de nacimiento inválida");
        RuleFor(x => x.UserId).MustAsync(async (username, cancellationToken) =>
            await repository.Query.AnyAsync(x => x.Username == username, cancellationToken)).WithMessage("Nombre de usuario no existe");
    }
}
