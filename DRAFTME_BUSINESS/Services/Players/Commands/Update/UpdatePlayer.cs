using DRAFTME_CORE.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DRAFTME_BUSINESS.Services.Players.Commands.Update;
public class UpdatePlayer : IRequest<PlayerDTO>
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public DateTime Nacimiento { get; set; }
    public string Posicion { get; set; }
    public string Biografia { get; set; }
    public string? UserId { get; set; }  // username
    public int? TeamId { get; set; }
    public int Goles { get; set; }
    public int Asistencias { get; set; }
    public int Minutos { get; set; }
    public IFormFile? Image { get; set; }
}

public class UpdatePlayerRequest
{
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public DateTime Nacimiento { get; set; }
    public string Posicion { get; set; }
    public string Biografia { get; set; }
    public string? UserId { get; set; }  // username
    public int? TeamId { get; set; }
    public int Goles { get; set; }
    public int Asistencias { get; set; }
    public int Minutos { get; set; }
    public IFormFile? Image { get; set; }
}
