using OTPService.Application.Messages;

namespace OTPService.Infrastructure.Messages;

public interface IRabbitMessageHandler
{
    public Task Handle(RabbitMessageEvent messageEvent, string messageJson);
}