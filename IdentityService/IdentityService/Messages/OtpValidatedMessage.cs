namespace IdentityService.Messages;

public class OtpValidatedMessage: RabbitMessage
{
    public override RabbitMessageEvent Event { get; set; } = RabbitMessageEvent.EVENT_OTP_VALIDATED;
    public Guid UserId { get; set; }
}