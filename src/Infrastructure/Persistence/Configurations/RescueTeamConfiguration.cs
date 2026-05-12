using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class RescueTeamConfiguration : IEntityTypeConfiguration<RescueTeam>
{
    public void Configure(EntityTypeBuilder<RescueTeam> builder)
    {
        builder.ToTable("rescue_teams");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
        builder.Property(x => x.LeaderPhone).HasColumnName("leader_phone").HasMaxLength(30);
        builder.Property(x => x.Status).HasColumnName("status").HasConversion<int>().IsRequired();
        builder.Property(x => x.CurrentLocation).HasColumnName("current_location").HasColumnType("geometry(Point,4326)");
        builder.Property(x => x.LastLocationUpdatedAt).HasColumnName("last_location_updated_at");
        builder.Property(x => x.Capacity).HasColumnName("capacity").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        builder.Property(x => x.DeletedAt).HasColumnName("deleted_at");

        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.LastLocationUpdatedAt);
        builder.HasIndex(x => new { x.Status, x.LastLocationUpdatedAt });

        builder.HasIndex(x => x.CurrentLocation)
            .HasMethod("gist")
            .HasDatabaseName("ix_rescue_teams_current_location_gist");

        builder.HasQueryFilter(x => x.DeletedAt == null);
    }
}

