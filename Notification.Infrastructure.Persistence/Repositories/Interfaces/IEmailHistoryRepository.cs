using Notification.Infrastructure.Domain.Entities;
using System.Linq.Expressions;

namespace Notification.Infrastructure.Persistence.Repositories.Interfaces;

public interface IEmailHistoryRepository
{
    IQueryable<EmailHistory> Get
        (
        Expression<Func<EmailHistory, bool>>? predicate = default,
        bool asNoTracking = false
        );

    ValueTask<EmailHistory> CreateAsync
        (
        EmailHistory emailHistory,
        bool saveChanges = true,
        CancellationToken cancellation = default
        );
}
