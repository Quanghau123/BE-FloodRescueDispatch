using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class SosRequestConfiguration : IEntityTypeConfiguration<SosRequest>
{
    public void Configure(EntityTypeBuilder<SosRequest> builder)
    {
        builder.ToTable("sos_requests");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CitizenId).HasColumnName("citizen_id").IsRequired();
        builder.Property(x => x.Location).HasColumnName("location").HasColumnType("geometry(Point,4326)").IsRequired();
        builder.Property(x => x.AddressText).HasColumnName("address_text").HasMaxLength(500);
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(2000);
        builder.Property(x => x.PeopleCount).HasColumnName("people_count").IsRequired();
        builder.Property(x => x.HasInjuredPeople).HasColumnName("has_injured_people").IsRequired();
        builder.Property(x => x.HasChildren).HasColumnName("has_children").IsRequired();
        builder.Property(x => x.HasElderly).HasColumnName("has_elderly").IsRequired();
        builder.Property(x => x.Status).HasColumnName("status").HasConversion<int>().IsRequired();
        builder.Property(x => x.PriorityScore).HasColumnName("priority_score").IsRequired();
        builder.Property(x => x.AssignedAt).HasColumnName("assigned_at");
        builder.Property(x => x.ResolvedAt).HasColumnName("resolved_at");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        builder.Property(x => x.DeletedAt).HasColumnName("deleted_at");

        builder.HasOne(x => x.Citizen)
            .WithMany(x => x.SosRequests)
            .HasForeignKey(x => x.CitizenId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.CitizenId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.CreatedAt);
        builder.HasIndex(x => x.PriorityScore);
        builder.HasIndex(x => new { x.Status, x.PriorityScore, x.CreatedAt })
            .HasDatabaseName("ix_sos_requests_status_priority_created_at");

        builder.HasIndex(x => x.Location)
            .HasMethod("gist")
            .HasDatabaseName("ix_sos_requests_location_gist");

        builder.HasQueryFilter(x => x.DeletedAt == null);
    }
}

