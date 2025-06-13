namespace DRAFTME_CORE.Models;
public class HistoricoPlayer
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public Player Player { get; set; }
    public string Temporada { get; set; }
    public int Goles { get; set; }
    public int Asistencias { get; set; }
    public int Minutos { get; set; }
    public int HistoricoTeamId { get; set; }
    public HistoricoTeam HistoricoTeam { get; set; }
}
