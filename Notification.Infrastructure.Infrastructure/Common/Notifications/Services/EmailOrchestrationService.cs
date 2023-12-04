using AutoMapper;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Domain.Common.Exceptions;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Extensions;

namespace Notification.Infrastructure.Infrastructure.Common.Notifications.Services;

public class EmailOrchestrationService : IEmailOrchestrationService
{
    private readonly IMapper _mapper;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IEmailRenderingService _emailRenderService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly IEmailHistoryService _emailHistoryService;

    public EmailOrchestrationService
        (
        IMapper mapper,
        IEmailTemplateService emailTemplateService,
        IEmailRenderingService emailRenderService,
        IEmailSenderService emailSenderService,
        IEmailHistoryService emailHistoryService
        )
    {
        _mapper = mapper;
        _emailTemplateService = emailTemplateService;
        _emailRenderService = emailRenderService;
        _emailSenderService = emailSenderService;
        _emailHistoryService = emailHistoryService;
    }
    public ValueTask<FuncResult<bool>> SendAsync(EmailNotificationRequest emailNotificationRequest, CancellationToken cancellationToken = default)
    {
        var sendNotificationRequest = async () =>
        {
            var message = _mapper.Map<EmailMessage>(emailNotificationRequest);

            message.Template = await _emailTemplateService.GetByTemplateType(emailNotificationRequest.TemplateType, cancellationToken: cancellationToken);

            await _emailRenderService.RenderAsync(message, cancellationToken: cancellationToken);

            await _emailSenderService.SendAsync(message, cancellationToken: cancellationToken);

            var history = _mapper.Map<EmailHistory>(message);
            await _emailHistoryService.CreateAsync(history, cancellationToken: cancellationToken);

            return history.IsSyccessful;
        };

        return sendNotificationRequest.GetValueAsync();
    }
}
