
namespace BookingService.Application.Messages;

public interface IRabbitMessage
{
    public RabbitMessageEvent Event { get; }
}