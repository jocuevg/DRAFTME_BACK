
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Enums;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Users.Commands.Update;
public class UpdateUser:IRequest<UserDTO>
{
    public string Username { get; set; }
    public string NewUsername { get; set; }
    public string PasswordHash { get; set; }
    public Rol Rol { get; set; }
}
