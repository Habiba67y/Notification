using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Application.Common.Notifications.Models;

public class SmsMessage : NotificationMessage
{
    public string SenderPhoneNumber { get; set; }
    public string ReceiverPhoneNumber { get; set; }
    public SmsTemplate Template { get; set; }
    public string Message { get; set; }
}
