using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Domain.Entities;

public class EmailTemplate : NotificationTemplate
{
    public EmailTemplate()
    {
        Type = NotificationType.Email;
    }
    public string Subject { get; set; } = default!;
}
