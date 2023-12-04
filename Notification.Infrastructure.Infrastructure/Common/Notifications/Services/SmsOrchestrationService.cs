using AutoMapper;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Domain.Common.Exceptions;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Extensions;

namespace Notification.Infrastructure.Infrastructure.Common.Notifications.Services;

public class SmsOrchestrationService : ISmsOrchestrationService
{
    private readonly IMapper _mapper;
    private readonly ISmsTemplateService _smsTemplateService;
    private readonly ISmsRenderingService _smsRenderService;
    private readonly ISmsSenderService _smsSenderService;
    private readonly ISmsHistoryService _smsHistoryService;

    public SmsOrchestrationService
        (
        IMapper mapper,
        ISmsTemplateService smsTemplateService,
        ISmsRenderingService smsRenderService,
        ISmsSenderService smsSenderService,
        ISmsHistoryService smsHistoryService
        )
    {
        _mapper = mapper;
        _smsTemplateService = smsTemplateService;
        _smsRenderService = smsRenderService;
        _smsSenderService = smsSenderService;
        _smsHistoryService = smsHistoryService;
    }
    public ValueTask<FuncResult<bool>> SendAsync
        (
        SmsNotificationRequest smsNotificationRequest, 
        CancellationToken cancellationToken = default
        )
    {
        var sendNotificationRequest = async () =>
        {
            var message = _mapper.Map<SmsMessage>(smsNotificationRequest);

            message.Template = await _smsTemplateService.GetByTemplateType(smsNotificationRequest.TemplateType, cancellationToken: cancellationToken);

            await _smsRenderService.RenderAsync(message, cancellationToken: cancellationToken);

            await _smsSenderService.SendAsync(message, cancellationToken: cancellationToken);

            var history = _mapper.Map<SmsHistory>(message);
            await _smsHistoryService.CreateAsync(history, cancellationToken: cancellationToken);

            return history.IsSyccessful;
        };

        return sendNotificationRequest.GetValueAsync();
    }
}
