using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Domain.Entities;

public class SmsHistory : NotificationHistory
{
    public SmsHistory()
    {
        Type = NotificationType.Sms;
    }
    public string SenderEmailAddress { get; set; } = default!;
    public string ReceiverEmailAddress { get; set; } = default!;
}
