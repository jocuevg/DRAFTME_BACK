using DRAFTME_CORE.Enums;

namespace DRAFTME_CORE.DTOs;
public class UserDTO
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public Rol Rol { get; set; }
}
