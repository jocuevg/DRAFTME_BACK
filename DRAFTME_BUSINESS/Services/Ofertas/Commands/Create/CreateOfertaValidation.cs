using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Ofertas.Commands.Create;
public class CreateOfertaValidation : AbstractValidator<CreateOferta>
{
    public CreateOfertaValidation(IRepository<Team> repositoryTeam)
    {
        RuleFor(x => x.Posicion).MaximumLength(20).WithMessage("Maximo posicion de 20 caracteres");
        RuleFor(x => x.Descripcion).MaximumLength(500).WithMessage("Maxima descripcion de 500 caracteres");
        RuleFor(x => x.TeamId).MustAsync(async (team, cancellationToken) =>
            await repositoryTeam.Query.AnyAsync(x => x.Id == team, cancellationToken)).WithMessage("Equipo no existe");
    }
}
