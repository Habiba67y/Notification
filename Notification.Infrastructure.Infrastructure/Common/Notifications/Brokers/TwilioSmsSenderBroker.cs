using Notification.Infrastructure.Application.Common.Notifications.Brokers;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Notification.Infrastructure.Infrastructure.Common.Notifications.Brokers;

public class TwilioSmsSenderBroker : ISmsSenderBroker
{
    public ValueTask<bool> SendAsync(SmsMessage message, CancellationToken cancellationToken = default)
    {
        var test = "ACe09f7247dfbdf25dbe2ef0acdf2279f9";
        var test2 = "e1fdedded3a1a2ddf74da5336dd7687d";

        TwilioClient.Init(test, test2);

        var messageContent = MessageResource.Create(
            body: message.Message,
            from: new Twilio.Types.PhoneNumber(message.SenderPhoneNumber),
            to: new Twilio.Types.PhoneNumber(message.ReceiverPhoneNumber)
        );

        return new ValueTask<bool>(true);
    }
}
