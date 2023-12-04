using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Domain.Entities;

public class SmsTemplate : NotificationTemplate
{
    public SmsTemplate()
    {
        Type = NotificationType.Sms;
    }
}
