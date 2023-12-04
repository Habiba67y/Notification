using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Persistence.EntityConfigurations;

public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        builder.Property(template => template.Subject).IsRequired().HasMaxLength(256);
    }
}
