using MediatR;

namespace DRAFTME_BUSINESS.Services.Historicos.Players.Commands.Delete;
public class DeleteHistoricoPlayer : IRequest
{
    public int Id { get; set; }
}
