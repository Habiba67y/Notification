using AutoMapper;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Infrastructure.Common.Mappers;

public class NotificationHistoryMapper : Profile
{
    public NotificationHistoryMapper()
    {
        CreateMap<EmailMessage, EmailHistory>();
        CreateMap<SmsMessage, SmsHistory>();
    }
}
