using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Application.Common.Notifications.Models;

public class EmailMessage : NotificationMessage
{
    public string SenderEmailAddress { get; set; }
    public string ReceiverEmailAddress { get; set; }
    public EmailTemplate Template { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
