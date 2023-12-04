using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Domain.Entities;

public class EmailHistory : NotificationHistory
{
    public EmailHistory()
    {
        Type = NotificationType.Email;
    }
    public string SenderEmailAddress { get; set; } = default!;
    public string ReceiverEmailAddress { get; set; } = default!;
    public string Subject { get; set; } = default!;
}
