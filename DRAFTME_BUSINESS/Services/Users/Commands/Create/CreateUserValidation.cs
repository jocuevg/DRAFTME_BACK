using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Users.Commands.Create;
public class CreateUserValidation : AbstractValidator<CreateUser>
{
    public CreateUserValidation(IRepository<User> repository)
    {
        RuleFor(x => x.Username).MaximumLength(20).WithMessage("Maximo nombre de usuario de 20 caracteres");
        RuleFor(x => x.Username).MustAsync(async (username, cancellationToken) =>
            await repository.Query.AllAsync(x => x.Username != username, cancellationToken)).WithMessage("Nombre de usuario ocupado");
        RuleFor(x => x.Rol).IsInEnum().WithMessage("El rol no es válido");
    }
}
