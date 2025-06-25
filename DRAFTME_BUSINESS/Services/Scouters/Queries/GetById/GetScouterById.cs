using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Scouters.Queries.GetById;
public class GetScouterById : IRequest<ScouterDTO>
{
    public int Id { get; set; }
}
