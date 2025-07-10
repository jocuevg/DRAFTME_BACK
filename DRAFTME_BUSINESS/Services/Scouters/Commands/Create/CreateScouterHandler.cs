using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Scouters.Commands.Create;
public class CreateScouterHandler(IRepository<Scouter> repository, IValidator<CreateScouter> validator, IMapper mapper) : IRequestHandler<CreateScouter, ScouterDTO>
{
    public async Task<ScouterDTO> Handle(CreateScouter request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }
        var scouter = new Scouter
        {
            Nombre = request.Nombre,
            Apellidos = request.Apellidos,
            Nacimiento = request.Nacimiento,
            Biografia = request.Biografia,
            UserId = request.UserId,
        };
        repository.Add(scouter);
        await repository.SaveChangesAsync();
        return mapper.Map<ScouterDTO>(scouter);
    }
}
