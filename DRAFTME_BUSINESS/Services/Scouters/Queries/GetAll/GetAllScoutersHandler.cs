using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Scouters.Queries.GetAll;
public class GetAllScoutersHandler(IRepository<Scouter> repository, IMapper mapper) : IRequestHandler<GetAllScouters, List<ScouterDTO>>
{
    public async Task<List<ScouterDTO>> Handle(GetAllScouters request, CancellationToken cancellationToken)
    {
        return await repository.Query.Select(x => mapper.Map<ScouterDTO>(x)).ToListAsync(cancellationToken);
    }
}
