using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Historicos.Players.Commands.Update;
public class UpdateHistoricoPlayer : IRequest<HistoricoPlayerDTO>
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public string Temporada { get; set; }
    public int Goles { get; set; }
    public int Asistencias { get; set; }
    public int Minutos { get; set; }
    public int? HistoricoTeamId { get; set; }
}

public class UpdateHistoricoPlayerRequest {
    public int PlayerId { get; set; }
    public string Temporada { get; set; }
    public int Goles { get; set; }
    public int Asistencias { get; set; }
    public int Minutos { get; set; }
    public int? HistoricoTeamId { get; set; }
}
