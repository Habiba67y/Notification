using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Application.Common.Notifications.Models;

public class EmailNotificationRequest : NotificationRequest
{
    EmailNotificationRequest() => Type = NotificationType.Email;
}
