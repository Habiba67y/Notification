using Microsoft.EntityFrameworkCore;
using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Persistence.DataContexts;

public class NotificationDbContext : DbContext
{
    DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();
    DbSet<SmsTemplate> SmsTemplates => Set<SmsTemplate>();
    DbSet<EmailHistory> EmailHistories => Set<EmailHistory>();
    DbSet<SmsHistory> SmsHistories => Set<SmsHistory>();

    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
    }
}
