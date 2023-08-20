
using BookingService.Application.Messages;

namespace BookingService.Infrastructure.Messages;

public class RabbitMessage: IRabbitMessage
{
    public virtual RabbitMessageEvent Event { get; set; }
}