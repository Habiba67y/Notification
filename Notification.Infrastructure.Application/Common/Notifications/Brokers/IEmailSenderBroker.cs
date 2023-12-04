﻿using Notification.Infrastructure.Application.Common.Notifications.Models;

namespace Notification.Infrastructure.Application.Common.Notifications.Brokers;

public interface IEmailSenderBroker
{
    ValueTask<bool> SendAsync(EmailMessage message, CancellationToken cancellationToken = default);
}
