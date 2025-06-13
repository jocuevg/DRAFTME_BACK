namespace DRAFTME_CORE.Models;
public class User
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public int Rol { get; set; } // player=0 o scout=1
}
