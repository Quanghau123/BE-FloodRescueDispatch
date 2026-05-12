using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class RescueAssignmentConfiguration : IEntityTypeConfiguration<RescueAssignment>
{
    public void Configure(EntityTypeBuilder<RescueAssignment> builder)
    {
        builder.ToTable("rescue_assignments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.SosRequestId).HasColumnName("sos_request_id").IsRequired();
        builder.Property(x => x.RescueTeamId).HasColumnName("rescue_team_id").IsRequired();
        builder.Property(x => x.Status).HasColumnName("status").HasConversion<int>().IsRequired();
        builder.Property(x => x.AssignedAt).HasColumnName("assigned_at").IsRequired();
        builder.Property(x => x.AcceptedAt).HasColumnName("accepted_at");
        builder.Property(x => x.ArrivedAt).HasColumnName("arrived_at");
        builder.Property(x => x.CompletedAt).HasColumnName("completed_at");
        builder.Property(x => x.Note).HasColumnName("note").HasMaxLength(1000);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasOne(x => x.SosRequest)
            .WithMany(x => x.Assignments)
            .HasForeignKey(x => x.SosRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.RescueTeam)
            .WithMany(x => x.Assignments)
            .HasForeignKey(x => x.RescueTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.SosRequestId);
        builder.HasIndex(x => x.RescueTeamId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.AssignedAt);
        builder.HasIndex(x => new { x.RescueTeamId, x.Status, x.AssignedAt });
    }
}

