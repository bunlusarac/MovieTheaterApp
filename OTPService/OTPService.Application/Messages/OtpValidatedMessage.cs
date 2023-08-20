namespace OTPService.Application.Messages;

public class OtpValidatedMessage: IRabbitMessage
{
    public RabbitMessageEvent Event => RabbitMessageEvent.EVENT_OTP_VALIDATED;
    public Guid UserId { get; set; }
}