﻿using Notification.Infrastructure.Application.Common.Notifications.Models;

namespace Notification.Infrastructure.Application.Common.Notifications.Services;

public interface ISmsSenderService
{
    ValueTask<bool> SendAsync(SmsMessage message, CancellationToken cancellationToken = default);
}
