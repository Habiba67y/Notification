using Notification.Infrastructure.Application.Common.Models.Querying;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Application.Common.Notifications.Services;

public interface ISmsTemplateService
{
    ValueTask<IList<SmsTemplate>> GetByFilterAsync
        (
        FilterPagination filterPagination,
        bool AsNoTracking = false,
        CancellationToken cancellationToken = default
        );

    ValueTask<SmsTemplate?> GetByTemplateType
        (
        NotificationTemplateType type,
        bool AsNoTracking = false,
        CancellationToken cancellationToken = default
        );

    ValueTask<SmsTemplate> CreateAsync
        (
        SmsTemplate smsTemplate,
        bool SaveChanges = true,
        CancellationToken cancellationToken = default
        );
}
