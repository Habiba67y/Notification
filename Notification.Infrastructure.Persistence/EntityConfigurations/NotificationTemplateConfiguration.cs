using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Persistence.EntityConfigurations;

public class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
{
    public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
    {
        builder.Property(template => template.Content).HasMaxLength(129_536);

        builder.HasIndex(template => template.TemplateType).IsUnique();

        builder
            .ToTable("NotificationTemplates")
            .HasDiscriminator(template => template.Type)
            .HasValue<EmailTemplate>(NotificationType.Email)
            .HasValue<SmsTemplate>(NotificationType.Sms);
    }
}
