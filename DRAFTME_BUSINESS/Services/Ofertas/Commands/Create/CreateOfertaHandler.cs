using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Ofertas.Commands.Create;
public class CreateOfertaHandler(IRepository<Oferta> repository, IValidator<CreateOferta> validator, IMapper mapper) : IRequestHandler<CreateOferta, OfertaDTO>
{
    public async Task<OfertaDTO> Handle(CreateOferta request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }
        var oferta = new Oferta
        {
            Posicion = request.Posicion,
            Descripcion = request.Descripcion,
            TeamId = request.TeamId,
        };
        repository.Add(oferta);
        await repository.SaveChangesAsync();
        return mapper.Map<OfertaDTO>(oferta);
    }
}
