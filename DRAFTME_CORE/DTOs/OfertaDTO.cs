namespace DRAFTME_CORE.DTOs;
public class OfertaDTO
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public TeamDTOSumarized Team { get; set; }
    public string Posicion { get; set; }
    public string Descripcion { get; set; }
}
