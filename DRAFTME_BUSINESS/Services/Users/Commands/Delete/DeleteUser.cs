using MediatR;

namespace DRAFTME_BUSINESS.Services.Users.Commands.Delete;
public class DeleteUser : IRequest
{
    public string Username { get; set; }
}
