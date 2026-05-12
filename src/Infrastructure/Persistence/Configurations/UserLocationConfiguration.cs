using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class UserLocationConfiguration : IEntityTypeConfiguration<UserLocation>
{
    public void Configure(EntityTypeBuilder<UserLocation> builder)
    {
        builder.ToTable("user_locations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(x => x.Location).HasColumnName("location").HasColumnType("geometry(Point,4326)").IsRequired();
        builder.Property(x => x.AccuracyMeters).HasColumnName("accuracy_meters");
        builder.Property(x => x.CapturedAt).HasColumnName("captured_at").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Locations)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CapturedAt);
        builder.HasIndex(x => new { x.UserId, x.CapturedAt });

        builder.HasIndex(x => x.Location)
            .HasMethod("gist")
            .HasDatabaseName("ix_user_locations_location_gist");
    }
}

