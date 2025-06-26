using MediatR;

namespace DRAFTME_BUSINESS.Services.Historicos.Teams.Commands.Delete;
public class DeleteHistoricoTeam : IRequest
{
    public int Id { get; set; }
}
