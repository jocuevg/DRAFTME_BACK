namespace DRAFTME_CORE.Models;
public class HistoricoTeam
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; }
    public string Temporada { get; set; }
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
    public int Clasificacion { get; set; }
    public int Puntos { get; set; }
}
