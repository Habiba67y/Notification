using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Persistence.EntityConfigurations;

public class SmsTemplateConfiguration : IEntityTypeConfiguration<SmsTemplate>
{
    public void Configure(EntityTypeBuilder<SmsTemplate> builder)
    {
        
    }
}
