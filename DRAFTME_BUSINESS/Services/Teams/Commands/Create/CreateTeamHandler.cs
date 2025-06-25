using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DRAFTME_BUSINESS.Services.Teams.Commands.Create;
public class CreateTeamHandler(IRepository<Team> repository, IValidator<CreateTeam> validator, IConfiguration configuration, Mapper mapper) 
    : IRequestHandler<CreateTeam, TeamDTO>
{
    private readonly string _folderPath = configuration["ImagesPath"];
    public async Task<TeamDTO> Handle(CreateTeam request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }

        string imageURL = null;
        if (request.Escudo != null && request.Escudo.Length != 0)
        {
            var fileExtension = Path.GetExtension(request.Escudo.FileName);
            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_folderPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Escudo.CopyToAsync(stream);
            }

            imageURL = $"/{newFileName}";
        }

        var team = new Team
        {
            Nombre = request.Nombre,
            Formacion = request.Formacion,
            NumSocios = request.NumSocios,
            Estadio = request.Estadio,
            CategoriaId = request.CategoriaId,
            Clasificacion = request.Clasificacion,
            Puntos = request.Puntos,
            Escudo = imageURL,
        };
        repository.Add(team);
        await repository.SaveChangesAsync();
        return mapper.Map<TeamDTO>(team);
    }
}
