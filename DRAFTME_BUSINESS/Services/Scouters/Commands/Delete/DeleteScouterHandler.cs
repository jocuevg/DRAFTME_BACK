using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Scouters.Commands.Delete;
public class DeleteScouterHandler(IRepository<Scouter> repository, IRepository<User> repositoryUser) : IRequestHandler<DeleteScouter>
{
    public async Task Handle(DeleteScouter request, CancellationToken cancellationToken)
    {
        var scouter = await repository.Query.Include(x=>x.User).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        repository.Delete(scouter);
        repositoryUser.Delete(scouter.User);
        await repository.SaveChangesAsync();
    }
}
