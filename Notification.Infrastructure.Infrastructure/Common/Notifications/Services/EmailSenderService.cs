using Notification.Infrastructure.Application.Common.Notifications.Brokers;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Domain.Extensions;

namespace Notification.Infrastructure.Infrastructure.Common.Notifications.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly IEnumerable<IEmailSenderBroker> _brokers;

    public EmailSenderService(IEnumerable<IEmailSenderBroker> brokers)
    {
        _brokers = brokers;
    }
    public async ValueTask<bool> SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
    {
        foreach (var broker in _brokers)
        {
            var smsNotificationTask = () => broker.SendAsync(message, cancellationToken);
            var result = await smsNotificationTask.GetValueAsync();

            if (result.IsSuccess)
                return true;
        }
        return false;
    }
}
