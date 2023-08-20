
namespace OTPService.Application.Messages;

public interface IRabbitMessage
{
    public RabbitMessageEvent Event { get; }
}