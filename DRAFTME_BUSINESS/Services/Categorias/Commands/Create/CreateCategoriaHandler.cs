using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DRAFTME_BUSINESS.Services.Categorias.Commands.Create;
public class CreateCategoriaHandler(IRepository<Categoria> repository, IValidator<CreateCategoria> validator, IConfiguration configuration, IMapper mapper)
    : IRequestHandler<CreateCategoria, CategoriaDTO>
{
    private readonly string _folderPath = configuration["ImagesPath"];
    public async Task<CategoriaDTO> Handle(CreateCategoria request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
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

        var categoria = new Categoria
        {
            Nombre = request.Nombre,
            Grupo = request.Grupo,
            Logo = imageURL,
        };
        repository.Add(categoria);
        await repository.SaveChangesAsync();
        return mapper.Map<CategoriaDTO>(categoria);
    }
}
