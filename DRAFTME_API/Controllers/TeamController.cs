using DRAFTME_BUSINESS.Services.Teams.Commands.Create;
using DRAFTME_BUSINESS.Services.Teams.Commands.Delete;
using DRAFTME_BUSINESS.Services.Teams.Commands.Update;
using DRAFTME_BUSINESS.Services.Teams.Queries.GetAll;
using DRAFTME_BUSINESS.Services.Teams.Queries.GetById;
using DRAFTME_CORE.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DRAFTME_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : BaseApiController
{
    [HttpGet("All")]
    public async Task<List<TeamDTOSumarized>> GetAllTeamsAsync()
    {
        return await Mediator.Send(new GetAllTeams());
    }

    [HttpGet("{id}")]
    public async Task<TeamDTO> GetTeamByIdAsync(int id)
    {
        return await Mediator.Send(new GetTeamById() { Id = id });
    }

    [HttpPost]
    public async Task<TeamDTO> Create([FromForm] CreateTeam create)
    {
        return await Mediator.Send(create);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await Mediator.Send(new DeleteTeam() { Id = id });
    }

    [HttpPut("{id}")]
    public async Task Update(int id, [FromForm] UpdateTeamRequest update)
    {
        await Mediator.Send(new UpdateTeam()
        {
            Id = id,
            Nombre = update.Nombre,
            CategoriaId = update.CategoriaId,
            Clasificacion = update.Clasificacion,
            Escudo = update.Escudo,
            Estadio = update.Estadio,
            Formacion = update.Formacion,
            NumSocios = update.NumSocios,
            Puntos = update.Puntos,
        });
    }
}
