using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DRAFTME_BUSINESS.Services.Categorias.Commands.Delete;
public class DeleteCategoriaHandler(IRepository<Categoria> repository, IRepository<HistoricoTeam> repositoryHistorico, IConfiguration configuration) : IRequestHandler<DeleteCategoria>
{
    private readonly string _folderPath = configuration["ImagesPath"];
    public async Task Handle(DeleteCategoria request, CancellationToken cancellationToken)
    {
        var categoria = await repository.Query.Include(x => x.Equipos).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        foreach (var item in categoria.Equipos)
        {
            item.CategoriaId = -1;
        }
        await repositoryHistorico.Query.Where(x => x.CategoriaId == request.Id).ForEachAsync(x => x.CategoriaId = -1);

        if (categoria.Logo != null)
        {
            var oldFilePath = Path.Combine(_folderPath, Path.GetFileName(categoria.Logo.TrimStart('/')));
            if (File.Exists(oldFilePath))
            {
                File.Delete(oldFilePath);
            }
        }

        repository.Delete(categoria);
        await repository.SaveChangesAsync();
    }
}
