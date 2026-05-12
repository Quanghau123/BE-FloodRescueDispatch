namespace Infrastructure.Notifications;

public sealed class NotificationMessage
{
    public string Type { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public object? Data { get; set; }
}

