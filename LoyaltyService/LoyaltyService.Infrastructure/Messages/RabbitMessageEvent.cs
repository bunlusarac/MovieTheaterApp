namespace LoyaltyService.Infrastructure.Messages;

public enum RabbitMessageEvent
{
    EVENT_USER_REGISTERED,
    EVENT_OTP_CONFIRMED,
    EVENT_REWARD_PURCHASE,
}