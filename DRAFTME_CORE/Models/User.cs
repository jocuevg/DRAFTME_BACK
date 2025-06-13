using DRAFTME_CORE.Enums;
using System.ComponentModel.DataAnnotations;

namespace DRAFTME_CORE.Models;
public class User
{
    [Key]
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public Rol Rol { get; set; } 
}
