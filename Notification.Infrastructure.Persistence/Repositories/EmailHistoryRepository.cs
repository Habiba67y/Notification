using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Persistence.DataContexts;
using Notification.Infrastructure.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Notification.Infrastructure.Persistence.Repositories;

public class EmailHistoryRepository : EntityRepositoryBase<EmailHistory, NotificationDbContext>, IEmailHistoryRepository
{
    public EmailHistoryRepository(NotificationDbContext dbContext) : base(dbContext)
    {

    }

    public IQueryable<EmailHistory> Get
        (
        Expression<Func<EmailHistory, bool>>? predicate = null,
        bool asNoTracking = false
        )
        =>
    base.Get(predicate, asNoTracking);

    public ValueTask<EmailHistory> CreateAsync
        (
        EmailHistory emailHistory,
        bool saveChanges = true,
        CancellationToken cancellation = default
        ) =>
    base.CreateAsync(emailHistory, saveChanges, cancellation);

}

