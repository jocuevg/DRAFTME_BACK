using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DRAFTME_BUSINESS.Services.Players.Commands.Delete;
public class DeletePlayerHandler(IRepository<Player> repository, IRepository<HistoricoPlayer> repositoryHistorico, IRepository<User> repositoryUser, IConfiguration configuration)
    : IRequestHandler<DeletePlayer>
{
    private readonly string _folderPath = configuration["ImagesPath"];
    public async Task Handle(DeletePlayer request, CancellationToken cancellationToken)
    {
        var player = await repository.Query.Include(x => x.Historico).Include(x=>x.User)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        foreach (var item in player.Historico)
        {
            repositoryHistorico.Delete(item);
        }

        if (player.Image!= null)
        {
            var oldFilePath = Path.Combine(_folderPath, Path.GetFileName(player.Image.TrimStart('/')));
            if (File.Exists(oldFilePath))
            {
                File.Delete(oldFilePath);
            }
        }

        repository.Delete(player);
        repositoryUser.Delete(player.User);
        await repository.SaveChangesAsync();
    }
}
