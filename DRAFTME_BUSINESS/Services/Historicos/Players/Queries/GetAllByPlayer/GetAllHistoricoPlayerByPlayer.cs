using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Historicos.Players.Queries.GetAllByPlayer;
public class GetAllHistoricoPlayerByPlayer: IRequest<List<HistoricoPlayerDTO>>
{
    public int PlayerId { get; set; }
}
