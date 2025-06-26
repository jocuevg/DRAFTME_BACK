using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Historicos.Teams.Commands.Delete;
public class DeleteHistoricoTeamHandler(IRepository<HistoricoTeam> repository) : IRequestHandler<DeleteHistoricoTeam>
{
    public async Task Handle(DeleteHistoricoTeam request, CancellationToken cancellationToken)
    {
        var historico = await repository.Query.Include(x => x.Plantilla).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        foreach (var item in historico.Plantilla)
        {
            item.HistoricoTeamId = null;
        }
        repository.Delete(historico);
        await repository.SaveChangesAsync();
    }
}
