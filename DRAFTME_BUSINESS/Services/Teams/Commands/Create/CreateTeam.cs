using DRAFTME_CORE.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DRAFTME_BUSINESS.Services.Teams.Commands.Create;
public class CreateTeam : IRequest<TeamDTO>
{
    public string Nombre { get; set; }
    public DateTime Formacion { get; set; }
    public int NumSocios { get; set; }
    public string Estadio { get; set; }
    public int CategoriaId { get; set; }
    public int Clasificacion { get; set; }
    public int Puntos { get; set; }
    public IFormFile? Escudo { get; set; }
}
