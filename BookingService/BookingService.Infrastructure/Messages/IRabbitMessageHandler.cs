using BookingService.Application.Messages;

namespace BookingService.Infrastructure.Messages;

public interface IRabbitMessageHandler
{
    public Task Handle(RabbitMessageEvent messageEvent, string messageJson);
}