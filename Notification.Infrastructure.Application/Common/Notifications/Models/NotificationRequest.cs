using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Application.Common.Notifications.Models;

public class NotificationRequest
{
    public Guid? SenderUserId { get; set; }
    public Guid ReceiverUserId { get; set; }
    public NotificationTemplateType TemplateType { get; set; }
    public NotificationType? Type { get; set; }
    public Dictionary<string, string> Variables { get; set; }
}
