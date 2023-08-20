namespace LoyaltyService.Infrastructure.Messages;

public interface IRabbitMessageHandler
{
    public Task Handle(RabbitMessageEvent messageEvent, string messageJson);
}