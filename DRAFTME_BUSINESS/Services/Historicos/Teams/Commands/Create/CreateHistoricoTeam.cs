using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Historicos.Teams.Commands.Create;
public class CreateHistoricoTeam : IRequest<HistoricoTeamDTO>
{
    public int TeamId { get; set; }
    public string Temporada { get; set; }
    public int CategoriaId { get; set; }
    public int Clasificacion { get; set; }
    public int Puntos { get; set; }
}
