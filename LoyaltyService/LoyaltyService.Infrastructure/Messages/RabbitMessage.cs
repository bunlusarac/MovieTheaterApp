
namespace LoyaltyService.Infrastructure.Messages;

public class RabbitMessage
{
    public virtual RabbitMessageEvent Event { get; set; }
}