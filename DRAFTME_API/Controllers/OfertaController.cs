using DRAFTME_BUSINESS.Services.Ofertas.Commands.Create;
using DRAFTME_BUSINESS.Services.Ofertas.Commands.Delete;
using DRAFTME_BUSINESS.Services.Ofertas.Commands.Update;
using DRAFTME_BUSINESS.Services.Ofertas.Queries.GetAll;
using DRAFTME_BUSINESS.Services.Ofertas.Queries.GetById;
using DRAFTME_CORE.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DRAFTME_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OfertaController : BaseApiController
{
    [HttpGet("All")]
    public async Task<List<OfertaDTO>> GetAllOfertasAsync()
    {
        return await Mediator.Send(new GetAllOfertas());
    }

    [HttpGet("{id}")]
    public async Task<OfertaDTO> GetOfertaByIdAsync(int id)
    {
        return await Mediator.Send(new GetOfertaById() { Id = id });
    }

    [HttpPost]
    public async Task<OfertaDTO> Create(CreateOferta create)
    {
        return await Mediator.Send(create);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await Mediator.Send(new DeleteOferta() { Id = id });
    }

    [HttpPut("{id}")]
    public async Task Update(int id, UpdateOfertaRequest update)
    {
        await Mediator.Send(new UpdateOferta()
        {
            Id = id,
            Posicion = update.Posicion,
            Descripcion = update.Descripcion,
            TeamId = update.TeamId,
        });
    }
}
