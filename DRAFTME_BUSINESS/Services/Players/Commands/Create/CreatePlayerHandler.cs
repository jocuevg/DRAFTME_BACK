using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DRAFTME_BUSINESS.Services.Players.Commands.Create;
public class CreatePlayerHandler(IRepository<Player> repository, IValidator<CreatePlayer> validator, IConfiguration configuration, Mapper mapper)
    : IRequestHandler<CreatePlayer, PlayerDTO>
{
    private readonly string _folderPath = configuration["ImagesPath"];
    public async Task<PlayerDTO> Handle(CreatePlayer request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }

        string imageURL = null;
        if (request.Image != null && request.Image.Length != 0)
        {
            var fileExtension = Path.GetExtension(request.Image.FileName);
            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_folderPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            imageURL = $"/{newFileName}";
        }

        var player = new Player
        {
            Nombre = request.Nombre,
            Apellidos = request.Apellidos,
            Nacimiento = request.Nacimiento,
            Posicion = request.Posicion,
            Biografia = request.Biografia,
            UserId = request.UserId,
            TeamId = request.TeamId,
            Goles = request.Goles,
            Asistencias = request.Asistencias,
            Minutos = request.Minutos,
            Image = imageURL,
        };
        repository.Add(player);
        await repository.SaveChangesAsync();
        return mapper.Map<PlayerDTO>(player);
    }
}
