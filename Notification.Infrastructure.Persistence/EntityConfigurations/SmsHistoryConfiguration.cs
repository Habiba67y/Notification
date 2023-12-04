using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Persistence.EntityConfigurations;

public class SmsHistoryConfiguration : IEntityTypeConfiguration<SmsHistory>
{
    public void Configure(EntityTypeBuilder<SmsHistory> builder)
    {
        builder.Property(template => template.SenderEmailAddress).IsRequired().HasMaxLength(256);
        builder.Property(template => template.ReceiverEmailAddress).IsRequired().HasMaxLength(256);
    }
}
