using Notification.Infrastructure.Application.Common.Models.Querying;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;
using System.Linq.Expressions;

namespace Notification.Infrastructure.Application.Common.Notifications.Services;

public interface IEmailTemplateService
{
    IQueryable<EmailTemplate> Get
        (
        Expression<Func<EmailTemplate, bool>>? predicate = default,
        bool asNoTracking = false
        );

    ValueTask<IList<EmailTemplate>> GetByFilterAsync
        (
        FilterPagination filterPagination,
        bool AsNoTracking = false,
        CancellationToken cancellationToken = default
        );

    ValueTask<EmailTemplate?> GetByTemplateType
        (
        NotificationTemplateType type,
        bool AsNoTracking = false,
        CancellationToken cancellationToken = default
        );

    ValueTask<EmailTemplate> CreateAsync
        (
        EmailTemplate emailTemplate,
        bool SaveChanges = true,
        CancellationToken cancellationToken = default
        );
}
