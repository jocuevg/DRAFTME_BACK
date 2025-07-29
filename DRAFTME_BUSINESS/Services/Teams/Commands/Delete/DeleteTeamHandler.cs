using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DRAFTME_BUSINESS.Services.Teams.Commands.Delete;
public class DeleteTeamHandler(IRepository<Team> repository, IRepository<HistoricoTeam> repositoryHistorico, IRepository<Oferta> repositoryOfertas, IConfiguration configuration)
    : IRequestHandler<DeleteTeam>
{
    private readonly string _folderPath = configuration["ImagesPath"];
    public async Task Handle(DeleteTeam request, CancellationToken cancellationToken)
    {
        var team = await repository.Query.Include(x => x.Historico).Include(x => x.Ofertas).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        foreach (var item in team.Historico) { 
            repositoryHistorico.Delete(item);
        }
        foreach (var item in team.Ofertas)
        {
            repositoryOfertas.Delete(item);
        }

        if (team.Escudo != null)
        {
            var oldFilePath = Path.Combine(_folderPath, Path.GetFileName(team.Escudo.TrimStart('/')));
            if (File.Exists(oldFilePath))
            {
                File.Delete(oldFilePath);
            }
        }

        repository.Delete(team);
        await repository.SaveChangesAsync();
    }
}
