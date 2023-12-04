using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Notification.Infrastructure.Application.Common.Models.Querying;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Application.Common.Querying.Extensions;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;
using Notification.Infrastructure.Infrastructure.Common.Validators;
using Notification.Infrastructure.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Notification.Infrastructure.Infrastructure.Common.Notifications.Services;

public class SmsTemplateService :  ISmsTemplateService
{
    private readonly ISmsTemplateRepository _smsTemplateRepository;
    private readonly IValidator<SmsTemplate> _smsTemplateValidator;

    public SmsTemplateService
    (
        ISmsTemplateRepository smsTemplateRepository,
        IValidator<SmsTemplate> smsTemplateValidator
    )        
    {
        _smsTemplateRepository = smsTemplateRepository;
        _smsTemplateValidator = smsTemplateValidator;
    }

    public IQueryable<SmsTemplate> Get
        (
        Expression<Func<SmsTemplate, bool>>? predicate = null,
        bool asNoTracking = false
        ) =>
    _smsTemplateRepository.Get(predicate, asNoTracking);

    public async ValueTask<IList<SmsTemplate>> GetByFilterAsync
        (
        FilterPagination filterPagination,
        bool AsNoTracking = false,
        CancellationToken cancellationToken = default
        )
    => await _smsTemplateRepository.Get().ApplyPagination(filterPagination).ToListAsync();

    public async ValueTask<SmsTemplate?> GetByTemplateType
        (
        NotificationTemplateType type,
        bool AsNoTracking = false,
        CancellationToken cancellationToken = default
        ) =>
    await _smsTemplateRepository.Get(template => template.TemplateType == type)
        .SingleOrDefaultAsync(cancellationToken);
    public ValueTask<SmsTemplate> CreateAsync
    (
        SmsTemplate smsTemplate,
        bool SaveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        var validationResult = _smsTemplateValidator.Validate(smsTemplate);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return _smsTemplateRepository.CreateAsync(smsTemplate, SaveChanges, cancellationToken);
    }
}
