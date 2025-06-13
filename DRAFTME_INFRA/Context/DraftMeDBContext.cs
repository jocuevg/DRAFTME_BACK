using DRAFTME_CORE.Models;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_INFRA.Context;
public class DraftMeDBContext : DbContext
{
    public DraftMeDBContext(DbContextOptions<DraftMeDBContext> options) : base(options)
    {
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<HistoricoPlayer> HistoricosPlayer { get; set; }
    public DbSet<HistoricoTeam> HistoricosTeam { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Scouter> Scouters { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<User> Users { get; set; }
}
