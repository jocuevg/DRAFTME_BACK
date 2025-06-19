using Microsoft.Extensions.DependencyInjection;

namespace DRAFTME_BUSINESS.Extensions;
public static class ServiceExtension
{
    public static void AddBusinessLayer(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(ServiceExtension).Assembly);
        });
        services.AddAutoMapper(typeof(ServiceExtension).Assembly);

    }
}
