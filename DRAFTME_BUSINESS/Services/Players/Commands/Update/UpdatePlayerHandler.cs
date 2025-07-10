using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DRAFTME_BUSINESS.Services.Players.Commands.Update;
public class UpdatePlayerHandler(IRepository<Player> repository, IValidator<UpdatePlayer> validator, IConfiguration configuration, IMapper mapper)
    : IRequestHandler<UpdatePlayer, PlayerDTO>
{
    private readonly string _folderPath = configuration["ImagesPath"];
    public async Task<PlayerDTO> Handle(UpdatePlayer request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }

        var player = await repository.Query
            .Include(x => x.User)
            .Include(x => x.Team)
                .ThenInclude(x => x.Categoria)
            .Include(x => x.Historico)
                .ThenInclude(x => x.HistoricoTeam)
                    .ThenInclude(x => x.Categoria)
            .Include(x => x.Historico)
                .ThenInclude(x => x.HistoricoTeam)
                    .ThenInclude(x => x.Team)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (player.Image != null)
        {
            var oldFilePath = Path.Combine(_folderPath, Path.GetFileName(player.Image.TrimStart('/')));
            if (File.Exists(oldFilePath))
            {
                File.Delete(oldFilePath);
            }
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

        player.Nombre = request.Nombre;
        player.Apellidos = request.Apellidos;
        player.Nacimiento = request.Nacimiento;
        player.Biografia = request.Biografia;
        if (request.UserId != null)
        {
            player.UserId = request.UserId;
            player.User.Username = request.UserId;
        }
        player.TeamId = request.TeamId;
        player.Goles = request.Goles;
        player.Asistencias = request.Asistencias;
        player.Minutos = request.Minutos;
        player.Image = imageURL;
        await repository.SaveChangesAsync();
        return mapper.Map<PlayerDTO>(player);
    }
}
