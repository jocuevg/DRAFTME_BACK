using DRAFTME_CORE.Enums;

namespace DRAFTME_CORE.Models;
public class Team
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Nombre { get; set; }
    public DateTime Formacion { get; set; }
    public int NumSocios { get; set; }
    public string Estadio { get; set; }
    public Categoria Categoria { get; set; }
    public int Puesto { get; set; }
    public List<HistoricoTeam> Historico { get; set; } = new();
    public List<Player> Plantilla { get; set; } = new();
    public string? Escudo { get; set; }
}
