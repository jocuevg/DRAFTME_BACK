namespace DRAFTME_CORE.DTOs;
public class TeamDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public DateTime Formacion { get; set; }
    public int NumSocios { get; set; }
    public string Estadio { get; set; }
    public int CategoriaId { get; set; }
    public CategoriaDTOSumarized Categoria { get; set; }
    public int Clasificacion { get; set; }
    public int Puntos { get; set; }
    public List<HistoricoTeamDTO> Historico { get; set; } = new();
    public List<OfertaDTO> Ofertas { get; set; } = new();
    public string? Escudo { get; set; }
}
