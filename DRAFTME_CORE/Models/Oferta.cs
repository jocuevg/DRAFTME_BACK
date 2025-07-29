namespace DRAFTME_CORE.Models;
public class Oferta
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; }
    public string Posicion { get; set; }
    public string Descripcion { get; set; }
}
