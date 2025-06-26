using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Historicos.Players.Queries.GetById;
public class GetHistoricoPlayerById : IRequest<HistoricoPlayerDTO>
{
    public int Id { get; set; }
}
