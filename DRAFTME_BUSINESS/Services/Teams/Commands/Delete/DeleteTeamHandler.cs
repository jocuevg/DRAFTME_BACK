using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Teams.Commands.Delete;
public class DeleteTeamHandler(IRepository<Team> repository, IRepository<HistoricoTeam> repositoryHistorico) : IRequestHandler<DeleteTeam>
{
    public async Task Handle(DeleteTeam request, CancellationToken cancellationToken)
    {
        var team = await repository.Query.Include(x=>x.Historico).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        foreach (var item in team.Historico) { 
            repositoryHistorico.Delete(item);
        }
        repository.Delete(team);
        await repository.SaveChangesAsync();
    }
}
