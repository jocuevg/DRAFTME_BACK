using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Ofertas.Commands.Delete;
public class DeleteOfertaHandler(IRepository<Oferta> repository) : IRequestHandler<DeleteOferta>
{
    public async Task Handle(DeleteOferta request, CancellationToken cancellationToken)
    {
        var oferta = await repository.Query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        repository.Delete(oferta);
        await repository.SaveChangesAsync();
    }
}
