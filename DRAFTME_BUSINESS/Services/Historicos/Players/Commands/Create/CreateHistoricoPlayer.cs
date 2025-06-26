using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Historicos.Players.Commands.Create;
public class CreateHistoricoPlayer : IRequest<HistoricoPlayerDTO>
{
    public int PlayerId { get; set; }
    public string Temporada { get; set; }
    public int Goles { get; set; }
    public int Asistencias { get; set; }
    public int Minutos { get; set; }
    public int? HistoricoTeamId { get; set; }
}
