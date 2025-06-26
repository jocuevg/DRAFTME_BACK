using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Historicos.Teams.Queries.GetById;
public class GetHistoticoTeamById : IRequest<HistoricoTeamDTO>
{
    public int Id { get; set; }
}
