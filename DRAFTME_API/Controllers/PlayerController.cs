using DRAFTME_BUSINESS.Services.Players.Commands.Create;
using DRAFTME_BUSINESS.Services.Players.Commands.Delete;
using DRAFTME_BUSINESS.Services.Players.Commands.Update;
using DRAFTME_BUSINESS.Services.Players.Queries.GetAll;
using DRAFTME_BUSINESS.Services.Players.Queries.GetById;
using DRAFTME_CORE.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DRAFTME_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : BaseApiController
{
    [HttpGet("All")]
    public async Task<List<PlayerDTOSumarized>> GetAllPlayersAsync()
    {
        return await Mediator.Send(new GetAllPlayers());
    }

    [HttpGet("{id}")]
    public async Task<PlayerDTO> GetPlayerByIdAsync(int id)
    {
        return await Mediator.Send(new GetPlayerById() { Id = id });
    }

    [HttpPost]
    public async Task<PlayerDTO> Create([FromForm] CreatePlayer create)
    {
        return await Mediator.Send(create);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await Mediator.Send(new DeletePlayer() { Id = id });
    }

    [HttpPut("{id}")]
    public async Task Update(int id, [FromForm] UpdatePlayerRequest update)
    {
        await Mediator.Send(new UpdatePlayer()
        {
            Id = id,
            Nombre = update.Nombre,
            Apellidos = update.Apellidos,
            Asistencias = update.Asistencias,
            Biografia = update.Biografia,
            Goles = update.Goles,
            Image = update.Image,
            Minutos = update.Minutos,
            Nacimiento = update.Nacimiento,
            Posicion = update.Posicion,
            TeamId = update.TeamId,
            UserId = update.UserId,
        });
    }
}
