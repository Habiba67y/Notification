using AutoMapper;
using Notification.Infrastructure.Application.Common.Models.Querying;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Domain.Common.Exceptions;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;
using Notification.Infrastructure.Domain.Extensions;

namespace Notification.Infrastructure.Infrastructure.Common.Notifications.Services;

public class NotificationAggregatorService : INotificationAggregatorService
{
    private readonly IMapper _mapper;
    private readonly IEmailOrchestrationService _emailOrchestrationService;
    private readonly ISmsOrchestrationService _smsOrchestrationService;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly ISmsTemplateService _smsTemplateService;

    public NotificationAggregatorService
        (
        IMapper mapper,
        IEmailOrchestrationService emailOrchestrationService,
        ISmsOrchestrationService smsOrchestrationService,
        IEmailTemplateService emailTemplateService,
        ISmsTemplateService smsTemplateService
        )
    {
        _mapper = mapper;
        _emailOrchestrationService = emailOrchestrationService;
        _smsOrchestrationService = smsOrchestrationService;
        _emailTemplateService = emailTemplateService;
        _smsTemplateService = smsTemplateService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync
        (
        NotificationRequest notificationRequest, 
        CancellationToken cancellationToken = default
        )
    {
        var sendNotificationTask = async () =>
        {
            var sendNotificationTask = notificationRequest.Type switch
            {
                NotificationType.Email => _emailOrchestrationService.SendAsync
                (
                    _mapper.Map<EmailNotificationRequest>(notificationRequest),
                    cancellationToken
                ),
                NotificationType.Sms => _smsOrchestrationService.SendAsync
                (
                    _mapper.Map<SmsNotificationRequest>(notificationRequest),
                    cancellationToken
                ),
                _ => throw new NotImplementedException("This type of notification is not supported yet.")
            };

            var sendNotificationResult = await sendNotificationTask;
            return sendNotificationResult.Data;
        };

        return await sendNotificationTask.GetValueAsync();
    }

    public async ValueTask<IList<NotificationTemplate>> GetTemplatesByFilterAsync
        (
        NotificationTemplateFilter notificationTemplateFilter, 
        CancellationToken cancellationToken = default
        )
    {
        var templates = new List<NotificationTemplate>();

        if(notificationTemplateFilter.TemplateType.Contains(NotificationType.Email))
            templates.AddRange(await _emailTemplateService.GetByFilterAsync(notificationTemplateFilter, 
                cancellationToken: cancellationToken));

        if (notificationTemplateFilter.TemplateType.Contains(NotificationType.Sms))
            templates.AddRange(await _smsTemplateService.GetByFilterAsync(notificationTemplateFilter,
                cancellationToken: cancellationToken));

        return templates;
    }
}
