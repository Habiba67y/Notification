﻿using Notification.Infrastructure.Application.Common.Notifications.Models;

namespace Notification.Infrastructure.Application.Common.Notifications.Services;

public interface IEmailRenderingService
{
    ValueTask<string> RenderAsync
        (
        EmailMessage emailMessage,
        CancellationToken cancellationToken = default
        );
}
