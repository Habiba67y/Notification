using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Persistence.DataContexts;
using Notification.Infrastructure.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Notification.Infrastructure.Persistence.Repositories;

public class SmsHistoryRepository : EntityRepositoryBase<SmsHistory, NotificationDbContext>, ISmsHistoryRepository
{
    public SmsHistoryRepository(NotificationDbContext dbContext) : base(dbContext)
    {

    }

    public IQueryable<SmsHistory> Get
        (
        Expression<Func<SmsHistory, bool>>? predicate = null,
        bool asNoTracking = false
        )
        =>
    base.Get(predicate, asNoTracking);

    public ValueTask<SmsHistory> CreateAsync
        (
        SmsHistory smsHistory,
        bool saveChanges = true,
        CancellationToken cancellation = default
        ) =>
    base.CreateAsync(smsHistory, saveChanges, cancellation);

}
