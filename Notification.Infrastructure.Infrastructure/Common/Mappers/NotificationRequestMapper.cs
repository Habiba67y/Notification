using AutoMapper;
using Notification.Infrastructure.Application.Common.Notifications.Models;

namespace Notification.Infrastructure.Infrastructure.Common.Mappers;

public class NotificationRequestMapper : Profile
{
    public NotificationRequestMapper()
    {
        CreateMap<NotificationRequest, EmailNotificationRequest>();
        CreateMap<NotificationRequest, SmsNotificationRequest>();   
    }
}
