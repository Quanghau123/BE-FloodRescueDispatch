using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class FloodZoneConfiguration : IEntityTypeConfiguration<FloodZone>
{
    public void Configure(EntityTypeBuilder<FloodZone> builder)
    {
        builder.ToTable("flood_zones");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
        builder.Property(x => x.Severity).HasColumnName("severity").HasConversion<int>().IsRequired();
        builder.Property(x => x.Status).HasColumnName("status").HasConversion<int>().IsRequired();
        builder.Property(x => x.Boundary).HasColumnName("boundary").HasColumnType("geometry(Polygon,4326)").IsRequired();
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(2000);
        builder.Property(x => x.EffectiveFrom).HasColumnName("effective_from").IsRequired();
        builder.Property(x => x.EffectiveTo).HasColumnName("effective_to");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        builder.Property(x => x.DeletedAt).HasColumnName("deleted_at");

        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.Severity);
        builder.HasIndex(x => x.CreatedAt);
        builder.HasIndex(x => new { x.Status, x.Severity });

        builder.HasIndex(x => x.Boundary)
            .HasMethod("gist")
            .HasDatabaseName("ix_flood_zones_boundary_gist");

        builder.HasQueryFilter(x => x.DeletedAt == null);
    }
}

