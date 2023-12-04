using Notification.Infrastructure.Application.Common.Notifications.Brokers;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Domain.Extensions;

namespace Notification.Infrastructure.Infrastructure.Common.Notifications.Services;

public class SmsSenderService : ISmsSenderService
{
    private readonly IEnumerable<ISmsSenderBroker> _brokers;

    public SmsSenderService(IEnumerable<ISmsSenderBroker> brokers)
    {
        _brokers = brokers;
    }
    public async ValueTask<bool> SendAsync(SmsMessage message, CancellationToken cancellationToken = default)
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
