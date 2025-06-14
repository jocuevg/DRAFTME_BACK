using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Users.Queries.GetById;
public class GetUserByUsername:IRequest<UserDTO>
{
    public string Username { get; set; }
}
