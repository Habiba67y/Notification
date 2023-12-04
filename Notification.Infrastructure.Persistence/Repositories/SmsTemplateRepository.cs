using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Persistence.DataContexts;
using Notification.Infrastructure.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Notification.Infrastructure.Persistence.Repositories;

public class SmsTemplateRepository : EntityRepositoryBase<SmsTemplate, NotificationDbContext>, ISmsTemplateRepository
{
    public SmsTemplateRepository(NotificationDbContext dbContext) : base(dbContext)
    {
        
    }

    public IQueryable<SmsTemplate> Get
        (
        Expression<Func<SmsTemplate, bool>>? predicate = null, 
        bool asNoTracking = false
        )
        =>
    base.Get( predicate, asNoTracking );
    public ValueTask<SmsTemplate> CreateAsync
        (
        SmsTemplate smsTemplate, 
        bool saveChanges = true, 
        CancellationToken cancellation = default
        ) =>
    base.CreateAsync( smsTemplate, saveChanges, cancellation );

}
