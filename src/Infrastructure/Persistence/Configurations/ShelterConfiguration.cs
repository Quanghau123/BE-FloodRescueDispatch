using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class ShelterConfiguration : IEntityTypeConfiguration<Shelter>
{
    public void Configure(EntityTypeBuilder<Shelter> builder)
    {
        builder.ToTable("shelters");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
        builder.Property(x => x.Address).HasColumnName("address").HasMaxLength(500).IsRequired();
        builder.Property(x => x.Location).HasColumnName("location").HasColumnType("geometry(Point,4326)").IsRequired();
        builder.Property(x => x.Capacity).HasColumnName("capacity").IsRequired();
        builder.Property(x => x.CurrentOccupancy).HasColumnName("current_occupancy").IsRequired();
        builder.Property(x => x.Status).HasColumnName("status").HasConversion<int>().IsRequired();
        builder.Property(x => x.ContactPhone).HasColumnName("contact_phone").HasMaxLength(30);
        builder.Property(x => x.HasMedicalSupport).HasColumnName("has_medical_support").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        builder.Property(x => x.DeletedAt).HasColumnName("deleted_at");

        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.CreatedAt);
        builder.HasIndex(x => new { x.Status, x.CurrentOccupancy });

        builder.HasIndex(x => x.Location)
            .HasMethod("gist")
            .HasDatabaseName("ix_shelters_location_gist");

        builder.HasQueryFilter(x => x.DeletedAt == null);
    }
}

