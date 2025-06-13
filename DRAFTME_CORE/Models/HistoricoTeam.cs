using DRAFTME_CORE.Enums;

namespace DRAFTME_CORE.Models;
public class HistoricoTeam
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public string Temporada { get; set; }
    public Categoria Categoria { get; set; }
    public int Puesto { get; set; }
    public List<HistoricoPlayer> Plantilla { get; set; } = new();
}
