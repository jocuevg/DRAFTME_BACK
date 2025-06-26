using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Historicos.Players.Commands.Delete;
public class DeleteHistoricoPlayerHandler(IRepository<HistoricoPlayer> repository) : IRequestHandler<DeleteHistoricoPlayer>
{
    public async Task Handle(DeleteHistoricoPlayer request, CancellationToken cancellationToken)
    {
        var historico = await repository.Query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        repository.Delete(historico);
        await repository.SaveChangesAsync();
    }
}
