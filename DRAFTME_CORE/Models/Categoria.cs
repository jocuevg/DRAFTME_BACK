﻿namespace DRAFTME_CORE.Models;
public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Grupo { get; set; }
    public string? Logo { get; set; }
    public List<Team> Equipos { get; set; } = new();
}
