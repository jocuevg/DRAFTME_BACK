using DRAFTME_CORE.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DRAFTME_BUSINESS.Services.Categorias.Commands.Create;
public class CreateCategoria : IRequest<CategoriaDTO>
{
    public string Nombre { get; set; }
    public int Grupo { get; set; }
    public IFormFile? Logo { get; set; }
}
