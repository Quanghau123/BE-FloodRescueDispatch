using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Text.Json;

namespace Migrators.PostgreSQL;

public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        string? connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") ??
            Environment.GetEnvironmentVariable("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            var appsettingsPath = Path.Combine(basePath, "appsettings.json");

            if (File.Exists(appsettingsPath))
            {
                try
                {
                    using var stream = File.OpenRead(appsettingsPath);
                    using var json = JsonDocument.Parse(stream);

                    if (json.RootElement.TryGetProperty("ConnectionStrings", out var connectionStrings) &&
                        connectionStrings.TryGetProperty("DefaultConnection", out var defaultConnection) &&
                        defaultConnection.ValueKind == JsonValueKind.String)
                    {
                        connectionString = defaultConnection.GetString();
                    }
                }
                catch
                {
                    // Ignore invalid JSON and fall back to default connection string.
                }
            }
        }

        connectionString ??=
            "Host=127.0.0.1;Port=5432;Database=flood_rescue_dispatch;Username=postgres;Password=Gauchina@123;Include Error Detail=true;Command Timeout=120;MaxPoolSize=200;";

        var builder = new DbContextOptionsBuilder<AppDbContext>();

        builder.UseNpgsql(
            connectionString,
            options =>
            {
                options.UseNetTopologySuite();
                options.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.GetName().Name);
            });

        return new AppDbContext(builder.Options);
    }
}