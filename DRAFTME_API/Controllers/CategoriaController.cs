using DRAFTME_BUSINESS.Services.Categorias.Commands.Create;
using DRAFTME_BUSINESS.Services.Categorias.Commands.Delete;
using DRAFTME_BUSINESS.Services.Categorias.Commands.Update;
using DRAFTME_BUSINESS.Services.Categorias.Queries.GetAll;
using DRAFTME_BUSINESS.Services.Categorias.Queries.GetById;
using DRAFTME_CORE.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DRAFTME_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : BaseApiController
{
    [HttpGet("All")]
    public async Task<List<CategoriaDTOSumarized>> GetAllCategoriasAsync()
    {
        return await Mediator.Send(new GetAllCategorias());
    }

    [HttpGet("{id}")]
    public async Task<CategoriaDTO> GetCategoriaByIdAsync(int id)
    {
        return await Mediator.Send(new GetCategoriaById() { Id = id });
    }

    [HttpPost]
    public async Task<CategoriaDTO> Create([FromForm] CreateCategoria createcategoria)
    {
        return await Mediator.Send(createcategoria);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await Mediator.Send(new DeleteCategoria() { Id = id });
    }

    [HttpPut("{id}")]
    public async Task Update(int id, [FromForm] UpdateCategoriaRequest updatecategoria)
    {
        await Mediator.Send(new UpdateCategoria()
        {
            Id = id,
            Nombre = updatecategoria.Nombre,
            Grupo = updatecategoria.Grupo,
            Logo = updatecategoria.Logo,
        });
    }
}
