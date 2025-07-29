using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Historicos.Teams.Queries.GetAllByTeam;
public class GetAllHistoricoTeamByTeam : IRequest<List<HistoricoTeamDTO>>
{
    public int TeamId { get; set; }
}
