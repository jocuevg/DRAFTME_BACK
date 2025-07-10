using DRAFTME_BUSINESS.Services.Historicos.Players.Commands.Create;
using DRAFTME_BUSINESS.Services.Historicos.Players.Commands.Delete;
using DRAFTME_BUSINESS.Services.Historicos.Players.Commands.Update;
using DRAFTME_BUSINESS.Services.Historicos.Players.Queries.GetAllByPlayer;
using DRAFTME_BUSINESS.Services.Historicos.Players.Queries.GetById;
using DRAFTME_CORE.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DRAFTME_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoricoPlayerController : BaseApiController
{
    [HttpGet("AllByPlayer/{id}")]
    public async Task<List<HistoricoPlayerDTO>> GetAllHistoricoPlayerByPlayerAsync(int id)
    {
        return await Mediator.Send(new GetAllHistoricoPlayerByPlayer() { PlayerId = id });
    }

    [HttpGet("{id}")]
    public async Task<HistoricoPlayerDTO> GetHistoricoPlayerByIdAsync(int id)
    {
        return await Mediator.Send(new GetHistoricoPlayerById() { Id = id });
    }

    [HttpPost]
    public async Task<HistoricoPlayerDTO> Create(CreateHistoricoPlayer createhistorico)
    {
        return await Mediator.Send(createhistorico);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await Mediator.Send(new DeleteHistoricoPlayer() { Id = id });
    }

    [HttpPut("{id}")]
    public async Task Update(int id, UpdateHistoricoPlayerRequest updatehistorico)
    {
        await Mediator.Send(new UpdateHistoricoPlayer()
        {
            Id = id,
            PlayerId = updatehistorico.PlayerId,
            Asistencias = updatehistorico.Asistencias,
            Goles = updatehistorico.Goles,
            HistoricoTeamId = updatehistorico.HistoricoTeamId,
            Minutos = updatehistorico.Minutos,
            Temporada = updatehistorico.Temporada,
        });
    }
}
