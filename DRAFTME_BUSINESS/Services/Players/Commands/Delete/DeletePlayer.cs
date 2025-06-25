using MediatR;

namespace DRAFTME_BUSINESS.Services.Players.Commands.Delete;
public class DeletePlayer : IRequest
{
    public int Id { get; set; }
}
