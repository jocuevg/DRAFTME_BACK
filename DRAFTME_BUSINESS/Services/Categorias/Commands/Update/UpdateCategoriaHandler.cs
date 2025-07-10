using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DRAFTME_BUSINESS.Services.Categorias.Commands.Update;
public class UpdateCategoriaHandler(IRepository<Categoria> repository, IValidator<UpdateCategoria> validator, IConfiguration configuration, IMapper mapper)
    : IRequestHandler<UpdateCategoria, CategoriaDTO>
{
    private readonly string _folderPath = configuration["ImagesPath"];
    public async Task<CategoriaDTO> Handle(UpdateCategoria request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }

        var categoria = await repository.Query.Include(x=>x.Equipos).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (categoria.Logo != null)
        {
            var oldFilePath = Path.Combine(_folderPath, Path.GetFileName(categoria.Logo.TrimStart('/')));
            if (File.Exists(oldFilePath))
            {
                File.Delete(oldFilePath);
            }
        }

        string imageURL = null;
        if (request.Logo != null && request.Logo.Length != 0)
        {
            var fileExtension = Path.GetExtension(request.Logo.FileName);
            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_folderPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Logo.CopyToAsync(stream);
            }

            imageURL = $"/{newFileName}";
        }

        categoria.Nombre = request.Nombre;
        categoria.Grupo = request.Grupo;
        categoria.Logo = imageURL;

        await repository.SaveChangesAsync();
        return mapper.Map<CategoriaDTO>(categoria);
    }
}
