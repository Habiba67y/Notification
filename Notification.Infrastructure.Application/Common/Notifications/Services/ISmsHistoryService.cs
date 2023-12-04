using Notification.Infrastructure.Application.Common.Models.Querying;
using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Application.Common.Notifications.Services;

public interface ISmsHistoryService
{
    ValueTask<IList<SmsHistory>> GetByFilter
        (
        FilterPagination filterPagination,
        bool AsNoTracking = false,
        CancellationToken cancellationToken = default
        );

    ValueTask<SmsHistory> CreateAsync
        (
        SmsHistory smsHistory,
        bool SaveChanges = true,
        CancellationToken cancellationToken = default
        );
}
