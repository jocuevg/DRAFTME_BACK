namespace DRAFTME_CORE.Models;
public class Scouter
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public DateTime Nacimiento { get; set; }
    public string Biografia { get; set; }
    public string UserId { get; set; }  // username
    public User User { get; set; }
    public int? TeamId { get; set; }
    public Team? Team { get; set; }
}
