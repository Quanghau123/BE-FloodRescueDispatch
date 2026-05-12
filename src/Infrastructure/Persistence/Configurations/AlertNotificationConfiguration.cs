using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class AlertNotificationConfiguration : IEntityTypeConfiguration<AlertNotification>
{
    public void Configure(EntityTypeBuilder<AlertNotification> builder)
    {
        builder.ToTable("alert_notifications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(x => x.FloodZoneId).HasColumnName("flood_zone_id");
        builder.Property(x => x.Severity).HasColumnName("severity").HasConversion<int>().IsRequired();
        builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        builder.Property(x => x.Message).HasColumnName("message").HasMaxLength(2000).IsRequired();
        builder.Property(x => x.IsRead).HasColumnName("is_read").IsRequired();
        builder.Property(x => x.ReadAt).HasColumnName("read_at");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.FloodZone)
            .WithMany()
            .HasForeignKey(x => x.FloodZoneId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.IsRead);
        builder.HasIndex(x => x.CreatedAt);
        builder.HasIndex(x => new { x.UserId, x.IsRead, x.CreatedAt });
    }
}

