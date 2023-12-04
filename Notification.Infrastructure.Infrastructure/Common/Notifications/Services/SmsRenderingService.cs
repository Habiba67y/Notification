using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Infrastructure.Common.Settings;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.Extensions.Options;

namespace Notification.Infrastructure.Infrastructure.Common.Notifications.Services;

public class SmsRenderingService : ISmsRenderingService
{
    private readonly TemplateRenderingSettings _templateRenderingSettings;

    public SmsRenderingService(IOptions<TemplateRenderingSettings> options)
    {
        _templateRenderingSettings = options.Value;
    }
    public ValueTask<string> RenderAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        var placeholderRegex = new Regex(_templateRenderingSettings.PlaceholderRegexPattern,
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(_templateRenderingSettings.RegexMatchTimeoutInSeconds));

        var placeholderValueRegex = new Regex(_templateRenderingSettings.PlaceholderValueRegexPattern,
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(_templateRenderingSettings.RegexMatchTimeoutInSeconds));

        var matches = placeholderRegex.Matches(smsMessage.Template.Content);

        var templatePlaceholders = matches.Select(match =>
        {
            var placeholder = match.Value;
            var placeholderValue = placeholderValueRegex.Match(placeholder).Groups[1].Value;
            var valid = smsMessage.Variables.TryGetValue(placeholderValue, out var value);

            return new TemplatePlaceholder
            {
                Placeholder = placeholder,
                PlaceholderValue = placeholderValue,
                Value = value,
                IsValid = valid
            };
        })
        .ToList();

        ValidatePlaceholders(templatePlaceholders);

        var messageBuilder = new StringBuilder(smsMessage.Template.Content);
        templatePlaceholders.ForEach(placeholder =>
        messageBuilder.Replace(placeholder.Placeholder, placeholder.Value));

        return new(messageBuilder.ToString());
    }

    private void ValidatePlaceholders(IEnumerable<TemplatePlaceholder> templatePlaceholders)
    {
        var missingPlaceholders = templatePlaceholders
            .Where(placeholder => !placeholder.IsValid)
            .Select(placeholder => placeholder.PlaceholderValue)
            .ToList();

        if (!missingPlaceholders.Any()) return;

        var errorMessage = new StringBuilder();
        missingPlaceholders.ForEach(placeholder => errorMessage.Append(placeholder).Append(','));

        throw new InvalidOperationException(
            $"Variable for given placeholders is not found - {string.Join(',', missingPlaceholders)}");
    }
}
