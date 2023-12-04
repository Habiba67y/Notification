using Notification.Infrastructure.Domain.Entities;
using System.Linq.Expressions;

namespace Notification.Infrastructure.Persistence.Repositories.Interfaces;

public interface ISmsHistoryRepository
{
    IQueryable<SmsHistory> Get
        (
        Expression<Func<SmsHistory, bool>>? predicate = default,
        bool asNoTracking = false
        );

    ValueTask<SmsHistory> CreateAsync
        (
        SmsHistory smsHistory,
        bool saveChanges = true,
        CancellationToken cancellation = default
        );
}
