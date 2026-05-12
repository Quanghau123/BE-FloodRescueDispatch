namespace Core.Application.Commands.Sos;

public sealed class CancelSosCommand
{
    public Guid SosRequestId { get; set; }

    public Guid CitizenId { get; set; }
}