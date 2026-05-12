using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> AppUsers => Set<AppUser>();

    public DbSet<UserLocation> UserLocations => Set<UserLocation>();

    public DbSet<FloodZone> FloodZones => Set<FloodZone>();

    public DbSet<Shelter> Shelters => Set<Shelter>();

    public DbSet<RescueTeam> RescueTeams => Set<RescueTeam>();

    public DbSet<RescueTeamLocationHistory> RescueTeamLocationHistories => Set<RescueTeamLocationHistory>();

    public DbSet<SosRequest> SosRequests => Set<SosRequest>();

    public DbSet<RescueAssignment> RescueAssignments => Set<RescueAssignment>();

    public DbSet<AlertNotification> AlertNotifications => Set<AlertNotification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}

