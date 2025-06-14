using MediatR;
using DRAFTME_CORE.DTOs;

namespace DRAFTME_BUSINESS.Services.Users.Queries.GetAll;
public class GetAllUsers :IRequest<List<UserDTO>>
{
}
