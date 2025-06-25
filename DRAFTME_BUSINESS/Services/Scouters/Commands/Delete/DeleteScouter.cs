using MediatR;

namespace DRAFTME_BUSINESS.Services.Scouters.Commands.Delete;
public class DeleteScouter : IRequest
{
    public int Id { get; set; }
}
