using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Teams.Queries.GetById;
public class GetTeamById : IRequest<TeamDTO>
{
    public int Id { get; set; }
}
