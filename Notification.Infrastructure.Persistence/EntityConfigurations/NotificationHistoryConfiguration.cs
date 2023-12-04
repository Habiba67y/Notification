using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Persistence.EntityConfigurations;

public class NotificationHistoryConfiguration : IEntityTypeConfiguration<NotificationHistory>
{
    public void Configure(EntityTypeBuilder<NotificationHistory> builder)
    {
        builder.Property(history => history.Content).HasMaxLength(129_536);

        builder
            .ToTable("NotificationTemplates")
            .HasDiscriminator(history => history.Type)
            .HasValue<EmailTemplate>(NotificationType.Email)
            .HasValue<SmsTemplate>(NotificationType.Sms);

        builder
            .HasOne<NotificationTemplate>(history => history.Template)
            .WithMany(template => template.Histories)
            .HasForeignKey(history => history.TemplateId);
    }
}
