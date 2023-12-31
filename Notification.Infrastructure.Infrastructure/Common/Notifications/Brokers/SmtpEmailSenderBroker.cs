﻿using Microsoft.Extensions.Options;
using Notification.Infrastructure.Application.Common.Notifications.Brokers;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using System.Net.Mail;
using System.Net;
using Notification.Infrastructure.Infrastructure.Common.Settings;

namespace Notification.Infrastructure.Infrastructure.Common.Notifications.Brokers;

public class SmtpEmailSenderBroker : IEmailSenderBroker
{
    private readonly SmtpEmailSenderSettings _smtpEmailSenderSettings;

    public SmtpEmailSenderBroker(IOptions<SmtpEmailSenderSettings> smtpEmailSenderSettings)
    {
        _smtpEmailSenderSettings = smtpEmailSenderSettings.Value;
    }

    public ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        emailMessage.SenderEmailAddress ??= _smtpEmailSenderSettings.CredentialAddress;

        var mail = new MailMessage(emailMessage.SenderEmailAddress, emailMessage.ReceiverEmailAddress);
        mail.Subject = emailMessage.Subject;
        mail.Body = emailMessage.Body;

        var smtpClient = new SmtpClient(_smtpEmailSenderSettings.Host, _smtpEmailSenderSettings.Port);
        smtpClient.Credentials =
            new NetworkCredential(_smtpEmailSenderSettings.CredentialAddress, _smtpEmailSenderSettings.Password);
        smtpClient.EnableSsl = true;

        smtpClient.Send(mail);

        return new ValueTask<bool>(true);
    }
}
