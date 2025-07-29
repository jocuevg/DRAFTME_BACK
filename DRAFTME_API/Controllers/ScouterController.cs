using DRAFTME_BUSINESS.Services.Scouters.Commands.Create;
using DRAFTME_BUSINESS.Services.Scouters.Commands.Delete;
using DRAFTME_BUSINESS.Services.Scouters.Commands.Update;
using DRAFTME_BUSINESS.Services.Scouters.Queries.GetAll;
using DRAFTME_BUSINESS.Services.Scouters.Queries.GetById;
using DRAFTME_CORE.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DRAFTME_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScouterController : BaseApiController
{
    [HttpGet("All")]
    public async Task<List<ScouterDTO>> GetAllScoutersAsync()
    {
        return await Mediator.Send(new GetAllScouters());
    }

    [HttpGet("{id}")]
    public async Task<ScouterDTO> GetScouterByIdAsync(int id)
    {
        return await Mediator.Send(new GetScouterById() { Id = id });
    }

    [HttpPost]
    public async Task<ScouterDTO> Create(CreateScouter create)
    {
        return await Mediator.Send(create);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await Mediator.Send(new DeleteScouter() { Id = id });
    }

    [HttpPut("{id}")]
    public async Task Update(int id, UpdateScouterRequest update)
    {
        await Mediator.Send(new UpdateScouter()
        {
            Id = id,
            Nombre = update.Nombre,
            Apellidos = update.Apellidos,
            Nacimiento = update.Nacimiento,
            Biografia = update.Biografia,
            UserId = update.UserId,
            TeamId = update.TeamId,
        });
    }
}
