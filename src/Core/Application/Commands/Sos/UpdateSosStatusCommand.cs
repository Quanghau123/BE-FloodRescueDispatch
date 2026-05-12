using Core.Domain.Enums;

namespace Core.Application.Commands.Sos;

public sealed class UpdateSosStatusCommand
{
    public Guid SosRequestId { get; set; }

    public SosStatus Status { get; set; }
}