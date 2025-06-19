using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Teams.Queries.GetAll;
public class GetAllTeamsHandler(IRepository<Team> repository, Mapper mapper) : IRequestHandler<GetAllTeams, List<TeamDTOSumarized>>
{
    public async Task<List<TeamDTOSumarized>> Handle(GetAllTeams request, CancellationToken cancellationToken)
    {
        return await repository.Query.Include(x=>x.Categoria).Select(x => mapper.Map<TeamDTOSumarized>(x)).ToListAsync(cancellationToken);
    }
}
