using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Persistence.DataContexts;
using Notification.Infrastructure.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Notification.Infrastructure.Persistence.Repositories;

public class EmailTemplateRepository : EntityRepositoryBase<EmailTemplate, NotificationDbContext>,  IEmailTemplateRepository
{
    public EmailTemplateRepository(NotificationDbContext dbContext) : base(dbContext)
    {
        
    }
    public IQueryable<EmailTemplate> Get
        (
        Expression<Func<EmailTemplate, bool>>? predicate = null, 
        bool asNoTracking = false
        ) =>
    base.Get( predicate, asNoTracking );

    public ValueTask<EmailTemplate> CreateAsync
        (
        EmailTemplate emailTemplate, 
        bool saveChanges = true, 
        CancellationToken cancellation = default
        ) =>
        base.CreateAsync( emailTemplate, saveChanges, cancellation );

}
