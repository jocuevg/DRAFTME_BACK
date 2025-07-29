namespace DRAFTME_CORE.DTOs;
public class HistoricoPlayerDTO
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public PlayerDTOSumarized Player { get; set; }
    public string Temporada { get; set; }
    public int Goles { get; set; }
    public int Asistencias { get; set; }
    public int Minutos { get; set; }
    public int? HistoricoTeamId { get; set; }
    public HistoricoTeamDTO? HistoricoTeam { get; set; }
}
