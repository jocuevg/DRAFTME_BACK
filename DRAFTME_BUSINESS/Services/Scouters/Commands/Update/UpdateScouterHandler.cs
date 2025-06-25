using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Scouters.Commands.Update;
public class UpdateScouterHandler(IRepository<Scouter> repository, IValidator<UpdateScouter> validator, Mapper mapper) : IRequestHandler<UpdateScouter, ScouterDTO>
{
    public async Task<ScouterDTO> Handle(UpdateScouter request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }
        var scouter = await repository.Query.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        scouter.Nombre = request.Nombre;
        scouter.Apellidos = request.Apellidos;
        scouter.Nacimiento = request.Nacimiento;
        scouter.Biografia = request.Biografia;
        if (request.UserId != null)
        {
            scouter.UserId = request.UserId;
            scouter.User.Username = request.UserId;
        }
        await repository.SaveChangesAsync();
        return mapper.Map<ScouterDTO>(scouter);
    }
}
