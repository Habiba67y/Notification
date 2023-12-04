using AutoMapper;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Infrastructure.Common.Mappers;

public class NotificationMessageMapper : Profile
{
    public NotificationMessageMapper()
    {
        CreateMap<EmailNotificationRequest, EmailMessage>();
        CreateMap<SmsNotificationRequest, SmsMessage>();
    }
}
