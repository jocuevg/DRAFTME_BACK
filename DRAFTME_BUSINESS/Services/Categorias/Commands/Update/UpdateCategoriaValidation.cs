using FluentValidation;

namespace DRAFTME_BUSINESS.Services.Categorias.Commands.Update;
public class UpdateCategoriaValidation : AbstractValidator<UpdateCategoria>
{
    public UpdateCategoriaValidation()
    {
        RuleFor(x => x.Nombre).MaximumLength(50).WithMessage("Maximo nombre de 50 caracteres");
        RuleFor(x => x.Grupo).GreaterThan(0).WithMessage("Numero de grupo invalido");
    }
}
