using DRAFTME_BUSINESS.Services.Teams.Commands.Create;
using DRAFTME_BUSINESS.Services.Teams.Commands.Update;
using DRAFTME_BUSINESS.Services.Users.Commands.Create;
using DRAFTME_BUSINESS.Services.Users.Commands.Update;
using FluentValidation;
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
        services.AddScoped(typeof(IValidator<CreateTeam>), typeof(CreateTeamValidation));
        services.AddScoped(typeof(IValidator<UpdateTeam>), typeof(UpdateTeamValidation));
        services.AddScoped(typeof(IValidator<CreateUser>), typeof(CreateUserValidation));
        services.AddScoped(typeof(IValidator<UpdateUser>), typeof(UpdateUserValidation));
        services.AddAutoMapper(typeof(ServiceExtension).Assembly);

    }
}
