using AutoMapper;
using DRAFTME_BUSINESS.Services.Teams.Commands.Create;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DRAFTME_BUSINESS.Services.Teams.Commands.Update;
public class UpdateTeamHandler(IRepository<Team> repository, IValidator<UpdateTeam> validator, IConfiguration configuration, Mapper mapper)
    : IRequestHandler<UpdateTeam, TeamDTO>
{
    private readonly string _folderPath = configuration["ImagesPath"];
    public async Task<TeamDTO> Handle(UpdateTeam request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }

        var team = await repository.Query
            .Include(x => x.Categoria)
            .Include(x => x.Historico)
                .ThenInclude(x => x.Categoria)
            .Include(x => x.Plantilla)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (team.Escudo != null)
        {
            var oldFilePath = Path.Combine(_folderPath, Path.GetFileName(team.Escudo.TrimStart('/')));
            if (File.Exists(oldFilePath))
            {
                File.Delete(oldFilePath);
            }
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

        team.Nombre = request.Nombre;
        team.Formacion = request.Formacion;
        team.NumSocios = request.NumSocios;
        team.Estadio = request.Estadio;
        team.CategoriaId = request.CategoriaId;
        team.Clasificacion = request.Clasificacion;
        team.Puntos = request.Puntos;
        team.Escudo = imageURL;

        await repository.SaveChangesAsync();
        return mapper.Map<TeamDTO>(team);
    }
}
