﻿using Notification.Infrastructure.Application.Common.Notifications.Models;

namespace Notification.Infrastructure.Application.Common.Notifications.Brokers;

public interface ISmsSenderBroker
{
    ValueTask<bool> SendAsync(SmsMessage message, CancellationToken cancellationToken = default);
}
