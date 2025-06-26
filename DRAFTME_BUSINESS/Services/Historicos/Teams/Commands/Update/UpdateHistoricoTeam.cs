using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Historicos.Teams.Commands.Update;
public class UpdateHistoricoTeam : IRequest<HistoricoTeamDTO>
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public string Temporada { get; set; }
    public int CategoriaId { get; set; }
    public int Clasificacion { get; set; }
    public int Puntos { get; set; }
}

public class UpdateHistoricoTeamRequest
{
    public int TeamId { get; set; }
    public string Temporada { get; set; }
    public int CategoriaId { get; set; }
    public int Clasificacion { get; set; }
    public int Puntos { get; set; }
}
