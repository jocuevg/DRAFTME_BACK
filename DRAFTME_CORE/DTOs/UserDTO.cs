using DRAFTME_CORE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRAFTME_CORE.DTOs;
public class UserDTO
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public Rol Rol { get; set; }
}
