using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DRAFTME_BUSINESS.Services.Ofertas.Commands.Update;
public class UpdateOfertaHandler(IRepository<Oferta> repository, IValidator<UpdateOferta> validator, IMapper mapper) : IRequestHandler<UpdateOferta, OfertaDTO>
{
    public async Task<OfertaDTO> Handle(UpdateOferta request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }
        var oferta = await repository.Query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        oferta.Posicion = request.Posicion;
        oferta.Descripcion = request.Descripcion;
        oferta.TeamId = request.TeamId;
        await repository.SaveChangesAsync();
        return mapper.Map<OfertaDTO>(oferta);
    }
}
