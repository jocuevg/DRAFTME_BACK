
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Users.Commands.Delete;
public class DeleteUserHandler(IRepository<User>repository) : IRequestHandler<DeleteUser>
{
    public async Task Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var user = await repository.Query.FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);
        repository.Delete(user);
        await repository.SaveChangesAsync();
    }
}
