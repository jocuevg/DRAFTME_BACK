namespace DRAFTME_CORE.DTOs;
public class TeamDTOSumarized
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int CategoriaId { get; set; }
    public CategoriaDTOSumarized Categoria { get; set; }
    public int Clasificacion { get; set; }
    public string? Escudo { get; set; }
}
