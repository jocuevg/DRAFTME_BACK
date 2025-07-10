using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Scouters.Queries.GetById;
public class GetScouterByIdHandler(IRepository<Scouter> repository, IMapper mapper) : IRequestHandler<GetScouterById, ScouterDTO>
{
    public async Task<ScouterDTO> Handle(GetScouterById request, CancellationToken cancellationToken)
    {
        var scouter = await repository.Query.Include(x=>x.User).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (scouter is null)
        {
            throw new KeyNotFoundException($"No se ha encontrado el scouter con id: {request.Id}");
        }
        else
        {
            return mapper.Map<ScouterDTO>(scouter);
        }
    }
}
