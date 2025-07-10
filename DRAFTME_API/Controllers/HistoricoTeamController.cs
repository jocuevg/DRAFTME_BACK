using DRAFTME_BUSINESS.Services.Historicos.Teams.Commands.Create;
using DRAFTME_BUSINESS.Services.Historicos.Teams.Commands.Delete;
using DRAFTME_BUSINESS.Services.Historicos.Teams.Commands.Update;
using DRAFTME_BUSINESS.Services.Historicos.Teams.Queries.GetAllByTeam;
using DRAFTME_BUSINESS.Services.Historicos.Teams.Queries.GetById;
using DRAFTME_CORE.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DRAFTME_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoricoTeamController : BaseApiController
{
    [HttpGet("AllByTeam/{id}")]
    public async Task<List<HistoricoTeamDTOSumarized>> GetAllHistoricoTeamByTeamAsync(int id)
    {
        return await Mediator.Send(new GetAllHistoricoTeamByTeam() { TeamId = id });
    }

    [HttpGet("{id}")]
    public async Task<HistoricoTeamDTO> GetHistoricoTeamByIdAsync(int id)
    {
        return await Mediator.Send(new GetHistoticoTeamById() { Id = id });
    }

    [HttpPost]
    public async Task<HistoricoTeamDTO> Create(CreateHistoricoTeam createhistorico)
    {
        return await Mediator.Send(createhistorico);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await Mediator.Send(new DeleteHistoricoTeam() { Id = id });
    }

    [HttpPut("{id}")]
    public async Task Update(int id, UpdateHistoricoTeamRequest updatehistorico)
    {
        await Mediator.Send(new UpdateHistoricoTeam()
        {
            Id = id,
            TeamId = updatehistorico.TeamId,
            CategoriaId = updatehistorico.CategoriaId,
            Clasificacion = updatehistorico.Clasificacion,
            Puntos = updatehistorico.Puntos,
            Temporada = updatehistorico.Temporada,
        });
    }
}
