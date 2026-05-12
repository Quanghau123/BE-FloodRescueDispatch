using Core.Application.Interfaces.Notifications;
using Core.Application.Interfaces.Persistence;
using Core.Application.Interfaces.PostGIS;
using Infrastructure.Notifications;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.PostGIS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                {
                    npgsqlOptions.UseNetTopologySuite();
                    npgsqlOptions.MigrationsAssembly("Migrators.PostgreSQL");
                });
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISosRepository, SosRepository>();
        services.AddScoped<IRescueTeamRepository, RescueTeamRepository>();
        services.AddScoped<IFloodZoneRepository, FloodZoneRepository>();
        services.AddScoped<IShelterRepository, ShelterRepository>();
        services.AddScoped<IGeoQueryService, PostGisGeoQueryService>();

        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
}

