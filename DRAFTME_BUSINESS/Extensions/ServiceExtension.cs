using DRAFTME_BUSINESS.Services.Categorias.Commands.Create;
using DRAFTME_BUSINESS.Services.Categorias.Commands.Update;
using DRAFTME_BUSINESS.Services.Historicos.Players.Commands.Create;
using DRAFTME_BUSINESS.Services.Historicos.Players.Commands.Update;
using DRAFTME_BUSINESS.Services.Historicos.Teams.Commands.Create;
using DRAFTME_BUSINESS.Services.Historicos.Teams.Commands.Update;
using DRAFTME_BUSINESS.Services.Players.Commands.Create;
using DRAFTME_BUSINESS.Services.Players.Commands.Update;
using DRAFTME_BUSINESS.Services.Scouters.Commands.Create;
using DRAFTME_BUSINESS.Services.Scouters.Commands.Update;
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
        services.AddScoped(typeof(IValidator<CreateScouter>), typeof(CreateScouterValidation));
        services.AddScoped(typeof(IValidator<UpdateScouter>), typeof(UpdateScouterValidation));
        services.AddScoped(typeof(IValidator<CreatePlayer>), typeof(CreatePlayerValidation));
        services.AddScoped(typeof(IValidator<UpdatePlayer>), typeof(UpdatePlayerValidation));
        services.AddScoped(typeof(IValidator<CreateCategoria>), typeof(CreateCategoriaValidation));
        services.AddScoped(typeof(IValidator<UpdateCategoria>), typeof(UpdateCategoriaValidation));
        services.AddScoped(typeof(IValidator<CreateHistoricoPlayer>), typeof(CreateHistoricoPlayerValidation));
        services.AddScoped(typeof(IValidator<UpdateHistoricoPlayer>), typeof(UpdateHistoricoPlayerValidation));
        services.AddScoped(typeof(IValidator<CreateHistoricoTeam>), typeof(CreateHistoricoTeamValidation));
        services.AddScoped(typeof(IValidator<UpdateHistoricoTeam>), typeof(UpdateHistoricoTeamValidation));

        services.AddAutoMapper(typeof(ServiceExtension).Assembly);

    }
}
