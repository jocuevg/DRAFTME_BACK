using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Historicos.Teams.Queries.GetAllByTeam;
public class GetAllHistoricoTeamByTeamHandler(IRepository<HistoricoTeam> repository, IMapper mapper)
    : IRequestHandler<GetAllHistoricoTeamByTeam, List<HistoricoTeamDTO>>
{
    public async Task<List<HistoricoTeamDTO>> Handle(GetAllHistoricoTeamByTeam request, CancellationToken cancellationToken)
    {
        return await repository.Query.Include(x => x.Categoria).Include(x => x.Team).Where(x => x.TeamId == request.TeamId)
            .Select(x => mapper.Map<HistoricoTeamDTO>(x)).ToListAsync(cancellationToken);
    }
}
