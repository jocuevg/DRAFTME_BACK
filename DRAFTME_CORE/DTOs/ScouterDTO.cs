namespace DRAFTME_CORE.DTOs;
public class ScouterDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public DateTime Nacimiento { get; set; }
    public string Biografia { get; set; }
    public string UserId { get; set; }  // username
    public UserDTO User { get; set; }
}
