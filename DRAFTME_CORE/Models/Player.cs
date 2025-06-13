namespace DRAFTME_CORE.Models;
public class Player
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public DateTime Nacimiento { get; set; }
    public string Posicion { get; set; }
    public string Biografia { get; set; }
    public string UserId { get; set; }  // username
    public User User { get; set; }
    public int? TeamId { get; set; }
    public Team? Team { get; set; }
    public int Goles { get; set; }
    public int Asistencias { get; set; }
    public int Minutos { get; set; }
    public List<HistoricoPlayer> Historico { get; set; } = new();
    public string? Image { get; set; }
}
