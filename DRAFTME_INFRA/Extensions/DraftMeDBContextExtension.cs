using DRAFTME_INFRA.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DRAFTME_INFRA.Extensions;
public static class DraftMeDBContextExtension
{
    public static void AddCustomDraftMeDBContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DraftMeDBContext>(options =>
        {
            options.UseMySql(configuration.GetConnectionString("DraftMe"), new MySqlServerVersion(new Version(8, 0, 0)),
            b => b.MigrationsAssembly(typeof(DraftMeDBContext).Assembly.FullName).CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds));
        });
    }
}
