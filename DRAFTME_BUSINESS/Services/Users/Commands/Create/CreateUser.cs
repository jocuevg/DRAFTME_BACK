using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Enums;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Users.Commands.Create;
public class CreateUser : IRequest<UserDTO>
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public Rol Rol { get; set; }
}
