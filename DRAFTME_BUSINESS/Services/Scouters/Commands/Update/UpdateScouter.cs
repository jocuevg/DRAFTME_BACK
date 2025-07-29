using DRAFTME_CORE.DTOs;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Scouters.Commands.Update;
public class UpdateScouter : IRequest<ScouterDTO>
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public DateTime Nacimiento { get; set; }
    public string Biografia { get; set; }
    public string? UserId { get; set; }
    public int? TeamId { get; set; }
}

public class UpdateScouterRequest
{
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public DateTime Nacimiento { get; set; }
    public string Biografia { get; set; }
    public string? UserId { get; set; }
    public int? TeamId { get; set; }
}
