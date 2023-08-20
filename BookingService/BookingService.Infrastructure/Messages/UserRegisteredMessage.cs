using BookingService.Application.Messages;

namespace BookingService.Infrastructure.Messages;

public class UserRegisteredMessage: RabbitMessage
{
    public override RabbitMessageEvent Event { get; set; } = RabbitMessageEvent.EVENT_USER_REGISTERED;
    public Guid UserId { get; set; }
}