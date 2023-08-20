namespace IdentityService.Messages;

public class RabbitMessage
{
    public virtual RabbitMessageEvent Event { get; set; }
}