namespace BookingService.Application.Messages;

public enum RabbitMessageEvent
{
    EVENT_USER_REGISTERED,
    EVENT_OTP_VALIDATED,
    EVENT_REWARD_PURCHASE,
}