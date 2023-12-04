﻿using Notification.Infrastructure.Domain.Common.Entities;
using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Domain.Entities;

public abstract class NotificationHistory : IEntity
{
    public Guid Id { get; set; }
    public Guid TemplateId { get; set; }
    public Guid SenderUserId { get; set; }
    public Guid ReceiverUserId { get; set; }
    public NotificationType Type { get; set; }
    public string Content { get; set; }
    public NotificationTemplate Template { get; set; }
    public bool IsSyccessful { get; set; }
    public string? ErrorMessage { get; set; }
}
