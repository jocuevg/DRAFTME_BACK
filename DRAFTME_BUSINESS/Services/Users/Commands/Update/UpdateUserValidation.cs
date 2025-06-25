using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Users.Commands.Update;
public class UpdateUserValidation : AbstractValidator<UpdateUser>
{
    public UpdateUserValidation(IRepository<User> repository)
    {
        RuleFor(x => x.Username).MustAsync(async (username, cancellationToken) =>
            await repository.Query.AnyAsync(x => x.Username == username, cancellationToken)).WithMessage("Usuario no existe");
        RuleFor(x => x.NewUsername).MaximumLength(20).WithMessage("Maximo nombre de usuario de 20 caracteres");
        RuleFor(x => x.NewUsername).MustAsync(async (username, cancellationToken) =>
            await repository.Query.AllAsync(x => x.Username != username, cancellationToken)).WithMessage("Nombre de usuario ocupado");
        RuleFor(x => x.Rol).IsInEnum().WithMessage("El rol no es válido");
    }
}
