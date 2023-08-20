using OTPService.Application.Messages;

namespace OTPService.Infrastructure.Communicators;

public class RabbitMessage: IRabbitMessage
{
    public RabbitMessageEvent Event { get; }
}