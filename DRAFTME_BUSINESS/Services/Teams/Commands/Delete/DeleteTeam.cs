using MediatR;

namespace DRAFTME_BUSINESS.Services.Teams.Commands.Delete;
public class DeleteTeam : IRequest
{
    public int Id { get; set; }
}
