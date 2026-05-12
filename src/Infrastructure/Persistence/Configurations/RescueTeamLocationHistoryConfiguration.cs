using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class RescueTeamLocationHistoryConfiguration : IEntityTypeConfiguration<RescueTeamLocationHistory>
{
    public void Configure(EntityTypeBuilder<RescueTeamLocationHistory> builder)
    {
        builder.ToTable("rescue_team_location_histories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.RescueTeamId).HasColumnName("rescue_team_id").IsRequired();
        builder.Property(x => x.Location).HasColumnName("location").HasColumnType("geometry(Point,4326)").IsRequired();
        builder.Property(x => x.CapturedAt).HasColumnName("captured_at").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasOne(x => x.RescueTeam)
            .WithMany(x => x.LocationHistories)
            .HasForeignKey(x => x.RescueTeamId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.RescueTeamId);
        builder.HasIndex(x => x.CapturedAt);
        builder.HasIndex(x => new { x.RescueTeamId, x.CapturedAt });

        builder.HasIndex(x => x.Location)
            .HasMethod("gist")
            .HasDatabaseName("ix_rescue_team_location_histories_location_gist");
    }
}

