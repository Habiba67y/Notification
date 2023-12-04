using FluentValidation;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Infrastructure.Common.Validators;

public class EmailTemplateValidator : AbstractValidator<EmailTemplate>
{
    public EmailTemplateValidator()
    {
        RuleFor(template => template.Content)
            .NotEmpty()
            .WithMessage("Email template content is required")
            .MinimumLength(10)
            .WithMessage("Email template content must be at least 10 characters long")
            .MaximumLength(256)
            .WithMessage("Email template content must be at most 256 characters long");

        RuleFor(template => template.Type)
            .Equal(NotificationType.Email)
            .WithMessage("Email template notification type must be Email");
    }
}
