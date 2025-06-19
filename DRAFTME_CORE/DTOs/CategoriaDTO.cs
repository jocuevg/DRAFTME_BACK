namespace DRAFTME_CORE.DTOs;
public class CategoriaDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Grupo { get; set; }
    public string? Logo { get; set; }
    public List<TeamDTOSumarized> Equipos { get; set; } = new();
}
