﻿namespace DRAFTME_CORE.DTOs;
public class HistoricoTeamDTOSumarized
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public TeamDTOSumarized Team { get; set; }
    public string Temporada { get; set; }
    public int CategoriaId { get; set; }
    public CategoriaDTOSumarized Categoria { get; set; }
    public int Clasificacion { get; set; }
}
