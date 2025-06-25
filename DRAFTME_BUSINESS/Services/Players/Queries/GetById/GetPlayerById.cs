using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Players.Queries.GetById;
public class GetPlayerById : IRequest<PlayerDTO>
{
    public int Id { get; set; }
}
