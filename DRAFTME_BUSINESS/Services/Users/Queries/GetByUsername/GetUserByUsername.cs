using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Users.Queries.GetUserByUsername;
public class GetUserByUsername : IRequest<UserDTO>
{
    public string Username { get; set; }
}
