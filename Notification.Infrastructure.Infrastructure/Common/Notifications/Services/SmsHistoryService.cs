using Microsoft.EntityFrameworkCore;
using Notification.Infrastructure.Application.Common.Models.Querying;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Application.Common.Querying.Extensions;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notification.Infrastructure.Infrastructure.Common.Notifications.Services;

public class SmsHistoryService : ISmsHistoryService
{
    private readonly ISmsHistoryRepository _repository;

    public SmsHistoryService(ISmsHistoryRepository repository)
    {
        _repository = repository;
    }
    public async ValueTask<IList<SmsHistory>> GetByFilter
        (
        FilterPagination filterPagination,
        bool AsNoTracking = false,
        CancellationToken cancellationToken = default
        ) =>
    await _repository.Get().ApplyPagination(filterPagination).ToListAsync(cancellationToken);


    public async ValueTask<SmsHistory> CreateAsync
        (
        SmsHistory smsHistory,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        ) =>
    await _repository.CreateAsync(smsHistory, saveChanges, cancellationToken);
}
