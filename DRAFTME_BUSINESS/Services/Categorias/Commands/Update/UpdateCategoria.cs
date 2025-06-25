using DRAFTME_CORE.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DRAFTME_BUSINESS.Services.Categorias.Commands.Update;
public class UpdateCategoria : IRequest<CategoriaDTO>
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Grupo { get; set; }
    public IFormFile? Logo { get; set; }
}

public class UpdateCategoriaRequest
{
    public string Nombre { get; set; }
    public int Grupo { get; set; }
    public IFormFile? Logo { get; set; }
}
