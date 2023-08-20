namespace IdentityService.Messages;

public interface IRabbitMessageHandler
{
    public Task Handle(RabbitMessageEvent messageEvent, string messageJson);
}