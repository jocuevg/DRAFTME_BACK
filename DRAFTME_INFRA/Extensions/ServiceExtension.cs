using DRAFTME_CORE.Interfaces;
using DRAFTME_INFRA.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DRAFTME_INFRA.Extensions;
public static class ServiceExtension
{
    public static void ConfigureInfraestructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
