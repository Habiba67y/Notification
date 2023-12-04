using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Notification.Infrastructure.Application.Common.Models.Querying;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Application.Common.Querying.Extensions;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;
using Notification.Infrastructure.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Notification.Infrastructure.Infrastructure.Common.Notifications.Services;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly IEmailTemplateRepository _emailTemplateRepository;
    private readonly IValidator<EmailTemplate> _emailTemplateValidator;

    public EmailTemplateService
        (
        IEmailTemplateRepository emailTemplateRepository,
        IValidator<EmailTemplate> emailTemplateValidator
        )
    {
        _emailTemplateRepository = emailTemplateRepository;
        _emailTemplateValidator = emailTemplateValidator;
    }

    public IQueryable<EmailTemplate> Get
        (
        Expression<Func<EmailTemplate, bool>>? predicate = null,
        bool asNoTracking = false
        ) =>
    _emailTemplateRepository.Get(predicate, asNoTracking);

    public async ValueTask<IList<EmailTemplate>> GetByFilterAsync
        (
        FilterPagination filterPagination,
        bool AsNoTracking = false,
        CancellationToken cancellationToken = default
        ) =>
    await _emailTemplateRepository.Get().ApplyPagination(filterPagination).ToListAsync();

    public async ValueTask<EmailTemplate?> GetByTemplateType
        (
        NotificationTemplateType type, 
        bool AsNoTracking = false, 
        CancellationToken cancellationToken = default
        ) =>
    await _emailTemplateRepository.Get(template => template.TemplateType == type)
        .SingleOrDefaultAsync(cancellationToken);

    public ValueTask<EmailTemplate> CreateAsync
        (
        EmailTemplate emailTemplate, 
        bool SaveChanges = true, 
        CancellationToken cancellationToken = default
        )
    {
        var validationResult = _emailTemplateValidator.Validate(emailTemplate);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return _emailTemplateRepository.CreateAsync(emailTemplate, SaveChanges, cancellationToken);
    }
}
