namespace DRAFTME_CORE.DTOs;
public class PlayerDTOSumarized
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public DateTime Nacimiento { get; set; }
    public string Posicion { get; set; }
    public int? TeamId { get; set; }
    public TeamDTOSumarized? Team { get; set; }
    public int Goles { get; set; }
    public int Asistencias { get; set; }
    public int Minutos { get; set; }
    public string? Image { get; set; }
}
